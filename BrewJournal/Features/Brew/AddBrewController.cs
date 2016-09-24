using System.Web.Mvc;

namespace BrewJournal.Features.Brew
{
    public class AddBrewController : Controller
    {
        public ActionResult Add()
        {
            return View(new AddBrewViewModel {Name = "Proper controller!!!!"});
        }

        [HttpPost]
        public ActionResult Add(AddBrewViewModel model)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}