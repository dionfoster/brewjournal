using System.Web.Mvc;

namespace BrewJournal.Features.Home
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}