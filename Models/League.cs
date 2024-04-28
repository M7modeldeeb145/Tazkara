namespace Tazaker.Models
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Match>? Matches { get; set; } 
        public List<Team>? Teams { get; set; }
    }
}
