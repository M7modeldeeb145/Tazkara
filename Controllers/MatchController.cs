using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Tazaker.Models;
using Tazkara.Data;
using Tazkara.IRepository;
using Tazkara.Models;
using Tazkara.ViewModels;

namespace Tazkara.Controllers
{
    [Authorize]
    public class MatchController : Controller
    {
        IMatch repository;
        IReservationCart cartrepos;
        IStadium stadiumrepo;
        private readonly ApplicationDbContext context;

        public MatchController(IMatch repository, IStadium stadiumrepo, IReservationCart cartrepos, ApplicationDbContext context)
        {
            this.repository = repository;
            this.stadiumrepo = stadiumrepo;
            this.cartrepos = cartrepos;
            this.context = context;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var Matches = repository.GetAll();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            int i = 1;

            foreach (var item in Matches)
            {
                var teamA = context.Teams.FirstOrDefault(e => e.Id == int.Parse(item.TeamA));
                var teamB = context.Teams.FirstOrDefault(e => e.Id == int.Parse(item.TeamB));

                var logoTeam1 = teamA?.TeamLogo;
                var NameTeam1 = teamA?.Name;
                var logoTeam2 = teamB?.TeamLogo;
                var NameTeam2 = teamB?.Name;

                dic.Add($"match{i}A", logoTeam1);
                dic.Add($"Name{i}A", NameTeam1);
                dic.Add($"match{i}B", logoTeam2);
                dic.Add($"Name{i++}B", NameTeam2);
            }
            ViewData["TeamsLogoAndNames"] = dic;
            ViewData["Leagues"] = repository.GetLeagues();
            ViewData["Stadiums"] = repository.GetStadiums();
            return View(Matches);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Teams"] = repository.GetTeams();
            ViewData["Stadiums"] = repository.GetStadiums();
            ViewData["Leagues"] = repository.GetLeagues();
            return View(new MatchViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MatchViewModel matchviewmodel)
        {
            if (ModelState.IsValid) 
            { 
                var match = new Tazaker.Models.Match()
                {
                    Name = matchviewmodel.Name,
                    LeagueId= matchviewmodel.LeagueId,
                    EndDate = matchviewmodel.EndDate,
                    StartDate = matchviewmodel.StartDate,
                    StadiumId = matchviewmodel.StadiumId,
                    TeamA = matchviewmodel.TeamA,
                    TeamB = matchviewmodel.TeamB,
                };
                repository.Create(match);
                return RedirectToAction("Index");
            }
            return View("Create");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var match = repository.GetById(id);
            if (match == null)
            {
                return RedirectToAction("Index");
            }
            var matchviewmodel = new MatchViewModel()
            {
                Name = match.Name,
                LeagueId = match.LeagueId,
                EndDate = match.EndDate,
                StartDate = match.StartDate,
                StadiumId= match.StadiumId,
                TeamA = match.TeamA,
                TeamB = match.TeamB,
            };
            ViewData["Teams"] = repository.GetTeams();
            ViewData["Stadiums"] = repository.GetStadiums();
            ViewData["Leagues"] = repository.GetLeagues();
            return View(matchviewmodel);
        }
        [HttpPost]
        public IActionResult Update(MatchViewModel matchviewmodel)
        {
            if (ModelState.IsValid)
            {
                var match = repository.GetById(matchviewmodel.Id);
                if(match == null)
                {
                    return NotFound();
                }
                //Update Match Properties
                match.StartDate = matchviewmodel.StartDate;
                match.EndDate = matchviewmodel.EndDate;
                match.TeamA = matchviewmodel.TeamA;
                match.TeamB = matchviewmodel.TeamB;
                match.Name = matchviewmodel.Name;
                match.LeagueId = matchviewmodel.LeagueId;
                match.StadiumId = matchviewmodel.StadiumId;
                repository.Update(match);
                return RedirectToAction("Index");
            }
            return View("Update");
        }
        //[HttpPost]
        public IActionResult Delete(int id)
        {
            var match = repository.GetById(id);
            repository.Delete(match.Id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Search (string name)
        {
            var matches = repository.Search(name);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            int i = 1;

            foreach (var item in matches)
            {
                var logoTeam1 = context.Teams.First(e => e.Id == int.Parse(item.TeamA)).TeamLogo;
                var NameTeam1 = context.Teams.First(e => e.Id == int.Parse(item.TeamA)).Name;
                var logoTeam2 = context.Teams.First(e => e.Id == int.Parse(item.TeamB)).TeamLogo;
                var NameTeam2 = context.Teams.First(e => e.Id == int.Parse(item.TeamB)).Name;

                dic.Add($"match{i}A", logoTeam1);
                dic.Add($"Name{i}A", NameTeam1);
                dic.Add($"match{i}B", logoTeam2);
                dic.Add($"Name{i++}B", NameTeam2);
            }
            ViewData["TeamsLogoAndNames"] = dic;
            ViewData["Leagues"] = repository.GetLeagues();

            return View(matches);
        }
        public IActionResult BookTicket(int id)
        {
            var match = repository.GetById(id);
            ReservationCart cart = new()
            {
                Match = match,
                Count = 1,
                MatchId = id,
                StadiumId = match.Stadium.Id,
                ReferenceNum = Guid.NewGuid(),
                Title = match.Name,
                EastPremiumStandsId = match.Stadium.EastPremiumStandsId,
                EastStandsId = match.Stadium.EastStandsId,
                CourtSidesRow3Id = match.Stadium.CourtSidesRow3Id,
                NorthPremiumStandsId = match.Stadium.NorthPremiumStandsId
            };

            ViewData["StadiumLocations"] = new Dictionary<string, int>
            {
                { "North Premium Stands", match.Stadium.NorthPremiumStandsId },
                { "East Premium Stands", match.Stadium.EastPremiumStandsId },
                { "East Stands", match.Stadium.EastStandsId },
                { "Court Sides Row 3", match.Stadium.CourtSidesRow3Id }
            };

            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult BookTicket(ReservationCart cart, string selectedLocation, int count)
        {
            var claimsidentity = (ClaimsIdentity)User.Identity;
            var userId = claimsidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            cart.ApplicationUserId = userId;

            switch (selectedLocation)
            {
                case "North Premium Stands":
                    var northPremiumStands = context.NorthPremiumStands.Find(cart.Stadium.NorthPremiumStandsId);
                    if (northPremiumStands.Capacity >= count)
                    {
                        northPremiumStands.Capacity -= count;
                        context.Update(northPremiumStands);
                        cart.NorthPremiumStandsId = cart.Stadium.NorthPremiumStandsId;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Not enough capacity in North Premium Stands");
                        return View(cart);
                    }
                    break;

                case "East Premium Stands":
                    var eastPremiumStands = context.EastPremiumStands.Find(cart.Stadium.EastPremiumStandsId);
                    if (eastPremiumStands.Capacity >= count)
                    {
                        eastPremiumStands.Capacity -= count;
                        context.Update(eastPremiumStands);
                        cart.EastPremiumStandsId = cart.Stadium.EastPremiumStandsId;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Not enough capacity in East Premium Stands");
                        return View(cart);
                    }
                    break;

                case "East Stands":
                    var eastStands = context.EastStands.Find(cart.Stadium.EastStandsId);
                    if (eastStands.Capacity >= count)
                    {
                        eastStands.Capacity -= count;
                        context.Update(eastStands);
                        cart.EastStandsId = cart.Stadium.EastStandsId;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Not enough capacity in East Stands");
                        return View(cart);
                    }
                    break;

                case "Court Sides Row 3":
                    var courtSidesRow3 = context.CourtSidesRow3.Find(cart.Stadium.CourtSidesRow3Id);
                    if (courtSidesRow3.Capacity >= count)
                    {
                        courtSidesRow3.Capacity -= count;
                        context.Update(courtSidesRow3);
                        cart.CourtSidesRow3Id = cart.Stadium.CourtSidesRow3Id;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Not enough capacity in Court Sides Row 3");
                        return View(cart);
                    }
                    break;
            }

            cart.Count = count;

            var existingCart = context.ReservationCarts
                .FirstOrDefault(e => e.ApplicationUserId == userId && e.MatchId == cart.MatchId);

            if (existingCart != null)
            {
                existingCart.Count += cart.Count;
                cartrepos.Update(existingCart);
            }
            else
            {
                cartrepos.Create(cart);
            }

            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
