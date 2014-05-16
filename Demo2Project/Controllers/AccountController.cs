using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Demo2Project.Models;
using Microsoft.Owin.Security;

namespace Demo2Project.Controllers
{
  //[Authorize]
  public class AccountController : Controller
  {
    private ApplicationDbContext ApplicationDbContext { get; set; }
    public UserManager<ApplicationUser> UserManager { get; private set; }

    private IAuthenticationManager AuthenticationManager
    {
      get
      {
        return HttpContext.GetOwinContext().Authentication;
      }
    }

    public AccountController()
      : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
    {
    }

    public AccountController(UserManager<ApplicationUser> userManager)
    {
      UserManager = userManager;
      ApplicationDbContext = new ApplicationDbContext();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && UserManager != null)
      {
        UserManager.Dispose();
        UserManager = null;
        ApplicationDbContext.Dispose();
        ApplicationDbContext = null;
      }
      base.Dispose(disposing);
    }

    //
    // GET: /Account/Login
    [AllowAnonymous]
    public ActionResult Login(string returnUrl)
    {
      ViewBag.ReturnUrl = returnUrl;
      return View();
    }

    //
    // GET: /Account/Logout
    [AllowAnonymous]
    public ActionResult Logout()
    {
      AuthenticationManager.SignOut();
      return RedirectToAction("Index", "Home");
    }

    //
    // POST: /Account/Login
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        var user = await UserManager.FindAsync(model.UserName, model.Password);
        if (user != null)
        {
          await SignInAsync(user, model.RememberMe);
          return RedirectToLocal(returnUrl);
        }
        ModelState.AddModelError("", "Invalid username or password.");
      }

      ApplicationContext.Message = new Message
      {
        typeClass = "message_validation",
        messageLines =
          ViewData.ModelState.Values.SelectMany(p_State => p_State.Errors.Select(p_Error => p_Error.ErrorMessage))
            .ToArray()
      };

      // If we got this far, something failed, redisplay form
      return View(model);
    }

    // GET: /Account/
    public ActionResult Index()
    {
      return View(UserManager.Users.Select(p_User => new AccountViewModel
      {
        UserName = p_User.UserName
      }));
    }

    // GET: /Account/Details/UserName
    public async Task<ActionResult> Details(string id)
    {
      if (id.IsNullOrWhiteSpace())
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ApplicationUser l_ApplicationUser = await UserManager.FindByNameAsync(id);
      if (l_ApplicationUser == null)
      {
        return HttpNotFound();
      }
      return View(
        new AccountViewModel
        {
          UserName = l_ApplicationUser.UserName
        });
    }

    // GET: /Account/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: /Account/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind(Include = "UserName")] AccountViewModel model)
    {
      if (ModelState.IsValid)
      {
        var l_User = new ApplicationUser() { UserName = model.UserName };
        var l_Result = await UserManager.CreateAsync(l_User, "Sanne123");
        if (l_Result.Succeeded)
        {
          //await SignInAsync(l_User, isPersistent: false);

          ApplicationContext.Message = new Message
          {
            typeClass = "message_success",
            messageLines = new[] { "Account created." },
          };

          return RedirectToAction("Index");
        }
        AddErrors(l_Result);
      }

      ApplicationContext.Message = new Message
      {
        typeClass = "message_validation",
        messageLines =
          ViewData.ModelState.Values.SelectMany(p_State => p_State.Errors.Select(p_Error => p_Error.ErrorMessage))
            .ToArray()
      };


      // If we got this far, something failed, redisplay form
      return View(model);
    }

    // GET: /Account/Delete/UserName
    [HttpGet]
    [ActionName("Delete")]
    public async Task<ActionResult> DeleteGet(string id)
    {
      if (id.IsNullOrWhiteSpace())
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ApplicationUser l_ApplicationUser = await UserManager.FindByNameAsync(id);
      if (l_ApplicationUser == null)
      {
        return HttpNotFound();
      }
      return View(new AccountViewModel { UserName = l_ApplicationUser.UserName });
    }

    // POST: /Account/Delete/UserName
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeletePost(string id)
    {
      if (id.IsNullOrWhiteSpace())
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ApplicationUser l_ApplicationUser = await UserManager.FindByNameAsync(id);
      if (l_ApplicationUser == null)
      {
        return HttpNotFound();
      }
      if (l_ApplicationUser.Id != HttpContext.User.Identity.GetUserId())
      {
        await UserManager.DeleteAsync(l_ApplicationUser);

        ApplicationContext.Message = new Message
        {
          typeClass = "message_success",
          messageLines = new[]
          {
            "Account deleted."
          },
        };

        return RedirectToAction("Index");
      }

      ApplicationContext.Message = new Message
      {
        typeClass = "message_validation",
        messageLines = new[]
          {
            "Cannot delete the current account."
          },
      };

      // If we got this far, something failed, redisplay form
      return View(new AccountViewModel { UserName = id });
    }

    // GET: /Account/Manage
    public ActionResult Manage()
    {
      if (!HttpContext.User.Identity.IsAuthenticated)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      return View(new AccountViewModel { UserName = HttpContext.User.Identity.GetUserName() });
    }

    private async Task SignInAsync(ApplicationUser user, bool isPersistent)
    {
      AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
      var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
      AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
    }

    private void AddErrors(IdentityResult result)
    {
      foreach (var l_Error in result.Errors)
      {
        ModelState.AddModelError("", l_Error);
      }
    }

    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      else
      {
        return RedirectToAction("Index", "Home");
      }
    }
  }
}