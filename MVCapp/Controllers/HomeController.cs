using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVCapp.Data;
using MVCapp.Models;
using MVCapp.ViewModels;
using System.Diagnostics;

namespace MVCapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            sortOrder = "Date";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
            List<Match> matches = await _context.Matches.Include(m => m.HomeTeam).Include(m => m.AwayTeam).ToListAsync(); ;
            matches = matches.OrderBy(m => m.Date).ToList();
            return View(matches);
        }



        public async Task<IActionResult> Favourite(int id)
        {
            //add match to user's favourites
            throw new NotImplementedException();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
