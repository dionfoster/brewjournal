using System.Web.Mvc;

namespace BrewJournal.Features.Home
{
    public class IndexHomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}