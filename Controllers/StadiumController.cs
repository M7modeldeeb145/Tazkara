using Microsoft.AspNetCore.Mvc;
using Tazaker.Models;
using Tazkara.IRepository;
using Tazkara.ViewModels;

namespace Tazkara.Controllers
{
    public class StadiumController : Controller
    {
        IStadium repository;
        public StadiumController(IStadium repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var stadiums = repository.GetAll();
            return View(stadiums);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StadiumViewModel stadiumViewModel)
        {
            if (ModelState.IsValid)
            {
                var stad = new Stadium
                {
                    Name = stadiumViewModel.Name,
                    Address = stadiumViewModel.Address,
                    Capacity = stadiumViewModel.Capacity,
                    TeamId = stadiumViewModel.TeamId,
                    StadiumStatus = stadiumViewModel.StadiumStatus,
                };
                repository.Create(stad);
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
            var stadVM = new StadiumViewModel 
            {
                Name = stadium.Name,
                Address = stadium.Address,
                Capacity = stadium.Capacity,
                TeamId = stadium.TeamId,
                StadiumStatus= stadium.StadiumStatus,
            };
            return View(stadVM);
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
                repository.Update(stadium);
                return RedirectToAction("Index");
            }
            return View("Update");
        }
        [HttpPost]
        public IActionResult Delete (int id)
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
