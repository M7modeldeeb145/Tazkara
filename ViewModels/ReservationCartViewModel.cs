using Tazkara.Models;

namespace Tazkara.ViewModels
{
    public class ReservationCartViewModel
    {
        public IEnumerable<ReservationCart> ReservationCarts { get; set; }
        public double Total { get; set; }
    }
}
