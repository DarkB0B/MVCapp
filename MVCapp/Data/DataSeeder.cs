using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MVCapp.Models;
using Newtonsoft.Json;
using System.Globalization;

namespace MVCapp.Data
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool IsLeaguesTableEmpty()
        {
            return !_context.Leagues.Any();
        }
        public bool IsTeamsTableEmpty()
        {
            return !_context.Teams.Any();
        }
        public bool IsMatchesTableEmpty()
        {
            return !_context.Matches.Any();
        }

        public async Task Seed()
        {
            await SeedLeagues();
            await SeedTeams();
            await SeedMatches();
        }

        public async Task SeedLeagues()
        {
            if (IsLeaguesTableEmpty())
            {
                Console.WriteLine("Seeding Leagues");
                using (StreamReader reader = new StreamReader("Data/Seed/Leagues.JSON"))
                {
                    string jsonData = await reader.ReadToEndAsync();
                    dynamic data = JsonConvert.DeserializeObject(jsonData);

                    foreach(var league in data.leagues)
                    {
                        League newLeague = new League
                        {
                            Name = league.name,
                        };
                        await _context.Leagues.AddAsync(newLeague);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                Console.WriteLine("Leagues table already has data");
            }         
        }
        public async Task SeedTeams()
        {
            if(IsTeamsTableEmpty())
            {
                Console.WriteLine("Seeding Teams");
                using (StreamReader reader = new StreamReader("Data/Seed/Teams.JSON"))
                {
                    string jsonData = await reader.ReadToEndAsync();
                    dynamic data = JsonConvert.DeserializeObject(jsonData);

                    foreach(var team in data.teams)
                    {
                        int? leagueId = team.leagueId;
                        if(leagueId == null)
                        {
                            Console.WriteLine($"{team.Name} has no league assigned");
                            continue;
                        }
                        League teamsLeague = await _context.Leagues.FirstOrDefaultAsync(l => l.Id == leagueId);
                        if(teamsLeague == null)
                        {
                            Console.WriteLine($"{team.Name} has wrong league assigned");
                            continue;
                        }
                        Team newTeam = new Team
                        {
                            Name = team.name,
                            League = teamsLeague
                        };
                        if (team.logo != null)
                        {
                            newTeam.Logo = team.logo;
                        }
                        await _context.Teams.AddAsync(newTeam);
                        Console.WriteLine($"{team.name} added to {teamsLeague.Name} with {team.Id}");
                    }
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                Console.WriteLine("Teams table already has data");
            }           
        }
        public async Task SeedMatches()
        {
            if (IsMatchesTableEmpty())
            {
                Console.WriteLine("Seeding Matches");
                using (StreamReader reader = new StreamReader("Data/Seed/Matches.JSON"))
                {
                    string jsonData = await reader.ReadToEndAsync();
                    dynamic data = JsonConvert.DeserializeObject(jsonData);

                    foreach(var match in data.matches)
                    {
                        int homeTeamId = match.homeTeamId;
                        int awayTeamId = match.awayTeamId;   
                        Console.WriteLine(awayTeamId);
                        string datex = match.date;
                        DateTime date = DateTime.ParseExact(datex, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        Match newMatch = new Match
                        {
                            HomeTeamId = homeTeamId,
                            AwayTeamId = awayTeamId,
                            HomeScore = match.homeScore,
                            AwayScore = match.awayScore,
                            Date = date,
                            leagueRound = match.leagueRound
                        };
                        await _context.Matches.AddAsync(newMatch);
                        
                    }
                    await _context.SaveChangesAsync();

                }
            }
            else
            {
                Console.WriteLine("Matches table already has data");
            }
        }
    }
}
