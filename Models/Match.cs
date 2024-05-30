using Tazkara.Models;

namespace Tazaker.Models
{
    public class Match
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string TeamA { get; set; } = null!;
        public string TeamB { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int LeagueId { get; set; }
        public League? League { get; set; }
        public int StadiumId { get; set; }
        public Stadium? Stadium { get; set; }
        public List<Team>? Teams { get; set; }
        public List<ReservationCart>? Tickets { get; set; }
    }
}
