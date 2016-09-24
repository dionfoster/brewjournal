using System.Web.Mvc;
using BrewJournal.Features.AddBrew.Models;

namespace BrewJournal.Features.AddBrew
{
    public class AddBrewController : Controller
    {
        public ActionResult AddBrew()
        {
            return View(new AddBrewViewModel());
        }

        [HttpPost]
        public ActionResult AddBrew(AddBrewViewModel model)
        {
            return View(model);
        }
    }
}