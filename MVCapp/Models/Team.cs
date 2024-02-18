namespace MVCapp.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public League League { get; set; }
        public List<Match> Matches { get; set; } = new List<Match>();

    }
}
