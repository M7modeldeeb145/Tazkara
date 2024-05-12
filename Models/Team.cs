namespace Tazaker.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string TeamLogo { get; set; } = null!;
        public List<Match> Matches { get; set; }
        public Stadium? Stadium { get; set; }
        public List<League>? Leagues { get; set; }
        
    }
}
