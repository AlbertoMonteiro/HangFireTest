using System.Web.Mvc;

namespace HangFireTest.Controllers
{
    public class HomeController : Controller
    {
        public static int message = 1;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = message;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}