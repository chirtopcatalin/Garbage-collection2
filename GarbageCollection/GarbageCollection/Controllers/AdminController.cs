using GarbageCollection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult AssignBinPage()
        {
            PopulateSelectLists();
            return View("AssignBin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignBin(BinCitizenModel model)
        {
            var existingAssignment = _context.BinCitizens
                                             .FirstOrDefault(bc => bc.IdBin == model.IdBin);

            if (existingAssignment != null)
            {
                ModelState.AddModelError("IdBin", "This bin is already assigned to a citizen.");
            }

            if (ModelState.IsValid)
            {
                _context.BinCitizens.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateSelectLists();
            return View("AssignBin", model);
        }


        private void PopulateSelectLists()
        {
            ViewBag.Citizens = new SelectList(_context.Citizens.Select(c => new {
                c.Id,
                FullName = c.FirstName + " " + c.LastName
            }), "Id", "FullName");

            ViewBag.Bins = new SelectList(_context.Bins, "Code", "Code");
        }

        public IActionResult UnassignBinPage()
        {
            var assignments = _context.BinCitizens
                .Join(_context.Citizens, bc => bc.IdCitizen, c => c.Id, (bc, c) => new
                {
                    bc.Id,
                    bc.IdBin,
                    CitizenName = c.FirstName + " " + c.LastName,
                    bc.Address
                })
                .ToList();

            return View("UnassignBin", assignments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UnassignBin(int id)
        {
            var assignment = _context.BinCitizens.FirstOrDefault(bc => bc.Id == id);
            if (assignment == null)
            {
                return NotFound();
            }

            _context.BinCitizens.Remove(assignment);
            _context.SaveChanges();

            return RedirectToAction("UnassignBinPage");
        }

        [HttpPost]
        [Route("api/addcollection")]
        [IgnoreAntiforgeryToken]
        public IActionResult AddCollectionApi([FromBody] List<CollectionModel> models)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var binCodes = models.Select(m => m.CodeBin).Distinct();
            var existingBins = _context.Bins
                .Where(b => binCodes.Contains(b.Code))
                .Select(b => b.Code)
                .ToHashSet();

            var validModels = models.Where(m => existingBins.Contains(m.CodeBin)).ToList();
            var invalidModels = models.Where(m => !existingBins.Contains(m.CodeBin)).ToList();

            if (validModels.Any())
            {
                _context.Collections.AddRange(validModels);
                _context.SaveChanges();
            }

            return Ok(new
            {
                message = "processed collections",
                addedCount = validModels.Count,
                skippedCount = invalidModels.Count,
                skippedBins = invalidModels.Select(m => m.CodeBin).Distinct(),
                addedIds = validModels.Select(m => m.Id)
            });
        }
    }
}
