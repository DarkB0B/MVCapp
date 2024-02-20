namespace MVCapp.ViewModels
{
    public class MatchViewModel
    {
        public int Id { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeTeamIcon { get; set; }
        public string AwayTeamName { get; set; }
        public string AwayTeamIcon { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public DateTime Date { get; set; }
        public int LeagueRound { get; set; }
    }
}
