namespace MVCapp.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }

        public League League { get; set; }
        public List<Match> HomeMatches { get; set; } = new List<Match>();

        public List<Match> AwayMatches { get; set; } = new List<Match>();

        public IEnumerable<Match> AllMatches => HomeMatches.Concat(AwayMatches);

    }
}
