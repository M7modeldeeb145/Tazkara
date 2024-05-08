namespace Tazkara.ViewModels
{
    public class MatchViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int LeagueId { get; set; }
        public int TeamId { get; set; }
        public int TicketId { get; set; }
    }
}
