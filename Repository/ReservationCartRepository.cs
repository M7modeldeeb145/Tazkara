using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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

        public void Create(ReservationCart cart)
        {
            cart.Id = 0;
            context.ReservationCarts.Add(cart);
            context.SaveChanges();
        }

        public IEnumerable<ReservationCart> GetAll()
        {
            return context.ReservationCarts.ToList();
        }

        public IEnumerable<ReservationCart> GetAllIfIdsEqual(string userId)
        {
            return context.ReservationCarts
        .Include(rc => rc.Stadium)
        .ThenInclude(s => s.NorthPremiumStands)
        .Include(rc => rc.Stadium)
        .ThenInclude(s => s.EastPremiumStands)
        .Include(rc => rc.Stadium)
        .ThenInclude(s => s.EastStands)
        .Include(rc => rc.Stadium)
        .ThenInclude(s => s.CourtSidesRow3)
        .Include(rc => rc.Match)
        .Where(rc => rc.ApplicationUserId == userId)
        .ToList();
        }

        public ReservationCart GetById(int id)
        {
            return context.ReservationCarts.FirstOrDefault(e=>e.Id == id);
        }

        public void Update(ReservationCart cart)
        {
            context.ReservationCarts.Update(cart);
            context.SaveChanges();
        }
    }
}
