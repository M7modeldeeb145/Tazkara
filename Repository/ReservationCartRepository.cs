using Tazkara.Data;
using Tazkara.IRepository;
using Tazkara.Models;

namespace Tazkara.Repository
{
    public class ReservationCartRepository : IReservationCart
    {
        private readonly ApplicationDbContext context;
        public ReservationCartRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Update(ReservationCart cart)
        {
            context.ReservationCarts.Update(cart);
        }
    }
}
