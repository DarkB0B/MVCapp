using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using MVCapp.Data;
using MVCapp.ViewModels;
using Microsoft.EntityFrameworkCore;
using MVCapp.Models;

namespace MVCapp.Controllers 
{
    public class MatchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(int? selectedRound, int? selectedLeagueId)
        {
            var leagues = _context.Leagues.ToList();
            
            

            var viewModel = new List<MatchViewModel>();
            if (!selectedLeagueId.HasValue && leagues != null && leagues.Count > 0)
            {
                selectedLeagueId = leagues.FirstOrDefault().Id;
            }
            var leagueMatches = await _context.Matches.Include(m => m.HomeTeam).Include(m => m.AwayTeam).Where(m =>  m.HomeTeam.League.Id == selectedLeagueId || m.AwayTeam.League.Id == selectedLeagueId).ToListAsync();
            var rounds = leagueMatches.Select(m => m.leagueRound).Distinct().ToList();
            if (!selectedRound.HasValue)
            {
                if (rounds.Count > 0)
                {
                    selectedRound = rounds.Last();
                }
                else
                {
                    return View();
                }
                
            }
            var roundMatches = leagueMatches.Where(m => m.leagueRound == selectedRound).ToList();
            

            viewModel = roundMatches.Select(m => new MatchViewModel
            {
                Id = m.Id,
                HomeTeamName = m.HomeTeam.Name,
                HomeTeamIcon = m.HomeTeam.Logo,
                AwayTeamName = m.AwayTeam.Name,
                AwayTeamIcon = m.AwayTeam.Logo,
                HomeScore = m.HomeScore,
                AwayScore = m.AwayScore,
                Date = m.Date,
                LeagueRound = m.leagueRound
            }).ToList();


            ViewBag.Rounds = new SelectList(rounds, selectedRound);
            ViewBag.Leagues = new SelectList(leagues, "Id", "Name", selectedLeagueId);
            return View(viewModel);
        }
    }
}
