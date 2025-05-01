using GarbageCollection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Data;

namespace GarbageCollection.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddCitizenPage()
        {
            return View("AddCitizen");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCitizen(CitizenModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Citizens.Add(model);
                _context.SaveChanges();
            }
            return View("Index");
        }


        public IActionResult DeleteCitizenPage()
        {
            var citizens = _context.Citizens.ToList();
            return View("DeleteCitizen", citizens);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCitizen(int id)
        {
            var citizen = _context.Citizens.FirstOrDefault(c => c.Id == id);

            if (citizen == null)
            {
                return NotFound();
            }

            var binCitizenLinks = _context.BinCitizens
                                          .Where(bc => bc.IdCitizen == id)
                                          .ToList();

            _context.BinCitizens.RemoveRange(binCitizenLinks);

            _context.Citizens.Remove(citizen);
            _context.SaveChanges();

            return RedirectToAction("DeleteCitizenPage");
        }


        public IActionResult AddBinPage()
        {
            return View("AddBin");
        }

        public IActionResult AddBin(BinModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Bins.Add(model);
                _context.SaveChanges();
            }
            return View("Index");
        }

        public IActionResult DeleteBinPage()
        {
            var bins = _context.Bins.ToList();
            return View("DeleteBin", bins);
        }

        public IActionResult DeleteBin(int id)
        {
            var bin = _context.Bins.FirstOrDefault(b => b.Id == id);
            if (bin != null)
            {
                var relatedAssignments = _context.BinCitizens.Where(bc => bc.IdBin == bin.Code).ToList();
                _context.BinCitizens.RemoveRange(relatedAssignments);
                _context.Bins.Remove(bin);
                _context.SaveChanges();
            }

            return View("DeleteBin");
        }

        [HttpGet]
        public IActionResult ViewCollectionsForCitizen(int citizenId)
        {
            var binIds = _context.BinCitizens
                                .Where(bc => bc.IdCitizen == citizenId)
                                .Select(bc => bc.IdBin)
                                .ToList();

            var binCodes = _context.Bins
                                .Where(b => binIds.Contains(b.Code))
                                .Select(b => b.Code)
                                .ToList();

            var collections = _context.Collections
                                    .Where(c => binCodes.Contains(c.CodeBin))
                                    .ToList();

            return View("CitizenCollections", collections);
        }

    }
}
