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
        public async Task<IActionResult> RouteMap()
        {
            const string CarNumber = "SB 42 ULB";

            var points = await _context.Collections
                .Where(c => c.car_number == CarNumber)
                .OrderBy(c => c.Id)
                .ToListAsync();

            return View(points);
        }
    }
}