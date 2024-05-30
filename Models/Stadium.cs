using Tazkara.Models;

namespace Tazaker.Models
{
    public class Stadium
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int TotalCapacity { get; set; }
        public string Address { get; set; } = null!;
        public StadiumStatus StadiumStatus { get; set; }
        public int TeamId { get; set; }
        public Team? team { get; set; }
        public Match? Match { get; set; }
        public List<ReservationCart>? ReservationCarts { get; set; }
        public EastPremiumStands? EastPremiumStands { get; set; }
        public int EastPremiumStandsId { get; set; }
        public NorthPremiumStands? NorthPremiumStands { get; set; }
        public int NorthPremiumStandsId { get; set; }
        public CourtSidesRow3? CourtSidesRow3 { get; set; }
        public int CourtSidesRow3Id { get; set; }
        public EastStands? EastStands { get; set; }
        public int EastStandsId { get; set; }
        // Method to update stadium status
        public void UpdateStadiumStatus()
        {
            if (Match != null && Match.EndDate.HasValue && Match.EndDate.Value < DateTime.Now)
            {
                StadiumStatus = StadiumStatus.Ended;
            }
            else if (ReservationCarts != null && ReservationCarts.Count > TotalCapacity)
            {
                StadiumStatus = StadiumStatus.Closed;
            }
            else
            {
                StadiumStatus = StadiumStatus.Avalible;
            }
        }
    }
}
