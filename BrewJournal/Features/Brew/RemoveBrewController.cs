using System;
using System.Linq;
using System.Web.Mvc;
using BrewJournal.EF;

namespace BrewJournal.Features.Brew
{
    public class RemoveBrewController : Controller
    {
        private readonly BrewContext _context;

        public RemoveBrewController(BrewContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult Remove(Guid id)
        {
            var brewToRemove = _context.Brews.FirstOrDefault(x => x.Id == id);

            if (brewToRemove == null)
                return RedirectToAction("Index", "Home");

            _context.Brews.Remove(brewToRemove);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}