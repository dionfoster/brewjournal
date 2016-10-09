using System;
using System.Collections.Generic;
using System.Linq;
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
            var brews = _context.Brews;

            var brewViewModels = new BrewListViewModel
            {
                Brews = brews
                    .Select(x => new BrewListItemViewModel
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                    .ToList()
            };  

            return View(brewViewModels);
        }
    }

    public class BrewListViewModel
    {
        public IList<BrewListItemViewModel> Brews { get; set; }
    }

    public class BrewListItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}