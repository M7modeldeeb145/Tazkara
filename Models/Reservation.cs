using Tazaker.Models;

namespace Tazkara.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime ReserveDateTime { get; set; }
        public int TicketId { get; set; }
        public Ticket ticket { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser user { get; set; }
    }
}
