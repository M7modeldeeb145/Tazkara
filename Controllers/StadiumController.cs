using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tazaker.Models;
using Tazkara.IRepository;
using Tazkara.ViewModels;

namespace Tazkara.Controllers
{
    [Authorize(Roles ="Admin")]

    public class StadiumController : Controller
    {
        IStadium repository;

        public StadiumController(IStadium repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var stadiums = repository.GetAll();
            ViewData["Teams"] = repository.GetAllTeams();
            return View(stadiums);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Teams"] = repository.GetAllTeams();
            ViewData["NorthPremium"] = repository.GetAllNorthPremiumStands();
            ViewData["EastPremium"] = repository.GetAllEastPremiumStands();
            ViewData["East"] = repository.GetAllEastStands();
            ViewData["CourtSidesRow3"] = repository.GetAllCourtSidesRow3();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StadiumViewModel stadiumViewModel)
        {
            if (ModelState.IsValid)
            {
                var stadium = new Stadium
                {
                    Name = stadiumViewModel.Name,
                    Address = stadiumViewModel.Address,
                    TotalCapacity = stadiumViewModel.TotalCapacity,
                    TeamId = stadiumViewModel.TeamId,
                    NorthPremiumStandsId = stadiumViewModel.NorthPremiumStandsId,
                    EastPremiumStandsId = stadiumViewModel.EastPremiumStandsId,
                    CourtSidesRow3Id = stadiumViewModel.CourtSidesRow3Id,
                    EastStandsId = stadiumViewModel.EastStandsId
                };

                // Call the method to update the stadium status
                stadium.UpdateStadiumStatus();

                repository.Create(stadium);
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var stadium = repository.GetById(id);
            if (stadium == null)
            {
                return NotFound();
            }
            var stadiumViewModel = new StadiumViewModel
            {
                Id = stadium.Id,
                Name = stadium.Name,
                Address = stadium.Address,
                TotalCapacity = stadium.TotalCapacity,
                TeamId = stadium.TeamId,
                StadiumStatus = stadium.StadiumStatus,
                NorthPremiumStandsId = stadium.NorthPremiumStandsId,
                EastPremiumStandsId = stadium.EastPremiumStandsId,
                CourtSidesRow3Id = stadium.CourtSidesRow3Id,
                EastStandsId = stadium.EastStandsId
            };
            ViewBag.Teams = repository.GetAllTeams();
            ViewBag.NorthPremium = repository.GetAllNorthPremiumStands();
            ViewBag.EastPremium = repository.GetAllEastPremiumStands();
            ViewBag.East = repository.GetAllEastStands();
            ViewBag.CourtSidesRow3 = repository.GetAllCourtSidesRow3();
            return View(stadiumViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(StadiumViewModel stadiumViewModel)
        {
            if (ModelState.IsValid)
            {
                var stadium = repository.GetById(stadiumViewModel.Id);
                if (stadium == null)
                {
                    return NotFound();
                }

                // Update the properties
                stadium.Name = stadiumViewModel.Name;
                stadium.Address = stadiumViewModel.Address;
                stadium.TotalCapacity = stadiumViewModel.TotalCapacity;
                stadium.TeamId = stadiumViewModel.TeamId;
                stadium.NorthPremiumStandsId = stadiumViewModel.NorthPremiumStandsId;
                stadium.EastPremiumStandsId = stadiumViewModel.EastPremiumStandsId;
                stadium.CourtSidesRow3Id = stadiumViewModel.CourtSidesRow3Id;
                stadium.EastStandsId = stadiumViewModel.EastStandsId;

                // Call the method to update the stadium status
                stadium.UpdateStadiumStatus();

                repository.Update(stadium);
                return RedirectToAction("Index");
            }
            return View("Update");
        }

        //[HttpPost]
        public IActionResult Delete(int id)
        {
            var stadium = repository.GetById(id);
            if (stadium == null)
            {
                return NotFound();
            }
            repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
