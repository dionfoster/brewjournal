using System.Web.Mvc;
using BrewJournal.EF;

namespace BrewJournal.Features.Home
{
    public class IndexHomeController : Controller
    {
        private readonly BrewContext _context;

        public IndexHomeController(BrewContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var brews = _context.Set<Domain.Brew>();

            return View();
        }
    }
}