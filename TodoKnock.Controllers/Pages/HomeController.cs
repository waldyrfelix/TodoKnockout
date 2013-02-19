using System.Web.Mvc;

namespace TodoKnock.Controllers.Pages
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
