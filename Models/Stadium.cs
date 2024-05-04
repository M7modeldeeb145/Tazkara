namespace Tazaker.Models
{
    public class Stadium
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public string Address { get; set; }=null!;
        public StadiumStatus StadiumStatus { get; set; }
        public int TeamId { get; set; }
        public Team? team { get; set; }
        public List<Match>? Matches { get; set; }
        public List<Ticket>? Tickets { get; set; }
        public EastPremiumStands EastPremiumStands { get; set; }
        public NorthPremiumStands NorthPremiumStands { get; set; }
    }
}
