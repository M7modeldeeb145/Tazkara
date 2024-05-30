using Tazkara.Models;

namespace Tazkara.IRepository
{
    public interface IReservationCart
    {
        void Update(ReservationCart cart);
        void Create(ReservationCart cart);
        ReservationCart GetById(int id);  
        IEnumerable<ReservationCart> GetAll();
        IEnumerable<ReservationCart> GetAllIfIdsEqual(string userId);
    }
}
