using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GarbageCollection.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using GarbageCollection.Data;

namespace GarbageCollection.Controllers
{
    public class RouteController : Controller
    {
        private readonly AppDbContext _context;

        public RouteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RouteMap(DateTime? date)
        {

            var query = _context.Collections.AsQueryable();

            if (date.HasValue)
            {
                var start = date.Value.Date;
                var end = start.AddDays(1);
                query = query.Where(c =>
                    c.CollectionTime >= start &&
                    c.CollectionTime < end);
            }
            else
            {
                return View("Index");
            }

                var points = await query
                    .OrderBy(c => c.Id)
                    .ToListAsync();

            return View(points);
        }
    }
}