using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Demo2Project.Models;
using WebGrease.Css.Extensions;

namespace Demo2Project.Controllers
{
  public class HomeController : Controller
  {
    //
    // GET: /Home/
    public ActionResult Index()
    {
      return View();
    }
  }
}