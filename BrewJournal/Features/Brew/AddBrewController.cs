﻿using System.Web.Mvc;
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
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddBrewViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var brewToAdd = new Domain.Brew(model.Name);

            _context.Set<Domain.Brew>().Add(brewToAdd);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}