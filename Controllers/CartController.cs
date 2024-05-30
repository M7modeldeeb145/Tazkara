using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Tazkara.IRepository;
using Tazkara.Models;
using Tazkara.ViewModels;

namespace Tazkara.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IReservationCart repository;
        private readonly IMatch repo;

        public CartController(IReservationCart repository, IMatch repo)
        {
            this.repository = repository;
            this.repo = repo;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var reservationCartVM = new ReservationCartViewModel
            {
                ReservationCarts = repository.GetAllIfIdsEqual(userId).ToList(),
                Total = 0 // Initialize total
            };

            foreach (var cart in reservationCartVM.ReservationCarts)
            {
                cart.Price = GetPriceBasedOnLocation(cart);
                reservationCartVM.Total += (cart.Price * cart.Count);
            }

            return View(reservationCartVM);
        }

        private double GetPriceBasedOnLocation(ReservationCart cart)
        {
            if (cart.Stadium == null)
            {
                return 0;
            }

            if (cart.Stadium.NorthPremiumStandsId == cart.NorthPremiumStandsId && cart.Stadium.NorthPremiumStands != null)
            {
                return cart.Stadium.NorthPremiumStands.Cost;
            }

            if (cart.Stadium.EastPremiumStandsId == cart.EastPremiumStandsId && cart.Stadium.EastPremiumStands != null)
            {
                return cart.Stadium.EastPremiumStands.Cost;
            }

            if (cart.Stadium.EastStandsId == cart.EastStandsId && cart.Stadium.EastStands != null)
            {
                return cart.Stadium.EastStands.Cost;
            }

            if (cart.Stadium.CourtSidesRow3Id == cart.CourtSidesRow3Id && cart.Stadium.CourtSidesRow3 != null)
            {
                return cart.Stadium.CourtSidesRow3.Cost;
            }

            // Default price if no valid stand type is found
            return 0;
        }
    }
}
