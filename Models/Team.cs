namespace Tazaker.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string TeamLogo { get; set; } = null!;
        public int MatchId { get; set; }
        public Match? Match { get; set; }
        public Stadium? Stadium { get; set; }
        public List<League>? Leagues { get; set; }
        
    }
}
