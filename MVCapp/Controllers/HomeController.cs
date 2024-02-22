using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCapp.Data;
using MVCapp.Models;
using MVCapp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index(int? selectedLeagueId)
        {
            var leagues = _context.Leagues.ToList();
            

            List<Team> teams = new List<Team>();
            if (!selectedLeagueId.HasValue && leagues != null && leagues.Count > 0)
            { 
                selectedLeagueId = leagues.FirstOrDefault().Id;
            }
            
                var selectedLeague = _context.Leagues
                    .Include(l => l.Teams)
                    .ThenInclude(t => t.HomeMatches)
                    .Include(l => l.Teams)
                    .ThenInclude(t => t.AwayMatches)
                    .FirstOrDefault(l => l.Id == selectedLeagueId.Value);

                if (selectedLeague != null)
                {
                    foreach (var team in selectedLeague.Teams)
                    {
                        teams.Add(team);
                    }
                }
            
            ViewBag.Leagues = new SelectList(leagues, "Id", "Name", selectedLeagueId);
            return View(teams);
        }
        public ActionResult GetMatches(int teamId)
        {
            var matches = _context.Matches.Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId).OrderByDescending(m => m.Date).Take(10).ToList();
            return PartialView("_MatchesPartial", matches);
        }
    }

}