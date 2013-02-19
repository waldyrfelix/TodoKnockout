using System.Web.Mvc;

namespace TodoKnock.Controllers.Pages
{
    public class AboutController : Controller
    {
        public ActionResult Index()
        {
            return Json(new {dados = "incorretos"}, JsonRequestBehavior.AllowGet);
        }
    }
}