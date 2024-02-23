using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCapp.Data;
using MVCapp.Models;
using Newtonsoft.Json;

namespace MVCapp.Controllers
{
    public class FavouritesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public FavouritesController(ApplicationDbContext conetxt, UserManager<User> userManager) 
        {
            _context = conetxt;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            Console.WriteLine(user.Email);
            Console.WriteLine(user.FavouriteTeams.Count);
            if(user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            List<Team> favTems = _context.Users.Include(t => t.FavouriteTeams).ThenInclude(t => t.HomeMatches).Include(t => t.FavouriteTeams).ThenInclude(t => t.AwayMatches).FirstOrDefault(u => u.Id == user.Id).FavouriteTeams;
            return View(favTems);
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
