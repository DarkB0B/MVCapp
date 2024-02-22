using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing;
using MVCapp.Data;
using MVCapp.Models;
using MVCapp.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public IActionResult GetTeamMatches(int teamId)
        {
            List<Match> teamMatches = _context.Matches
                .Where(match => match.HomeTeamId == teamId || match.AwayTeamId == teamId)
                .Include(t => t.HomeTeam)
                .Include(tt => tt.AwayTeam)
                .ToList();
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None
            };

            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(teamMatches, settings),
                ContentType = "application/json"
            };
            return jsonResult;
        }

    }

}