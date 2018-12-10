using System.Web.Mvc;

namespace CodingChallenge.UI.Controllers
{ 
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}