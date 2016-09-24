using System.Web.Mvc;
using BrewJournal.EF;

namespace BrewJournal.Features.Brew
{
    public class AddBrewController : Controller
    {
        private readonly BrewContext _context;

        public AddBrewController(BrewContext context)
        {
            _context = context;
        }

        public ActionResult Add()
        {
            return View(new AddBrewViewModel {Name = "Proper controller!!!!"});
        }

        [HttpPost]
        public ActionResult Add(AddBrewViewModel model)
        {
            var brewToAdd = new Domain.Brew(model.Name);

            _context.Set<Domain.Brew>().Add(brewToAdd);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}