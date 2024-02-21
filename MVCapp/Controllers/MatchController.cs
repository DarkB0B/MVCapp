using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using MVCapp.Data;
using MVCapp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MVCapp.Controllers 
{
    public class MatchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(int? selectedRound)
        {
            var rounds = await _context.Matches.Select(m => m.leagueRound).Distinct().ToListAsync();

            var viewModel = new List<MatchViewModel>();

            if (!selectedRound.HasValue)
            {
                selectedRound = rounds.Last();
            }
            var matches = await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Where(m => m.leagueRound == selectedRound.Value)
                .ToListAsync();

            viewModel = matches.Select(m => new MatchViewModel
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

            return View(viewModel);
        }
    }
}
