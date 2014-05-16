using System.Web.Mvc;
using System.Web.SessionState;

namespace Demo2Project.Controllers
{
  [SessionState(SessionStateBehavior.Required)]
  public class ApiController : Controller
  {
    //
    // GET: /Api/GetMessage/
    public ActionResult GetMessage()
    {
      if (ApplicationContext.Message == null)
      {
        return null;
      }
      JsonResult l_JsonResult = Json(ApplicationContext.Message);
      l_JsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
      ApplicationContext.Message = null;
      return l_JsonResult;
    }
  }
}