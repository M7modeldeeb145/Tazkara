using Microsoft.AspNetCore.Mvc;
using Tazaker.Models;
using Tazkara.IRepository;
using Tazkara.ViewModels;

namespace Tazkara.Controllers
{
    public class LeagueController : Controller
    {
        ILeague repository;
        public LeagueController(ILeague repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var leagues = repository.GetAll();
            return View(leagues);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LeagueViewModel leagueViewModel)
        {
            if (ModelState.IsValid)
            {
                var league = new League()
                {
                    Name = leagueViewModel.Name,
                };
                repository.Create(league);
                return RedirectToAction("Index");
            }
            return View("Create");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var league = repository.GetById(id);
            if (league == null)
            {
                return NotFound();
            }
            var leagueVM = new LeagueViewModel()
            {
                Name = league.Name, 
            };
            return View(leagueVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(LeagueViewModel leagueViewModel)
        {
            if (ModelState.IsValid) 
            {
                var league = repository.GetById(leagueViewModel.Id);
                if (league == null)
                {
                    return NotFound();
                }
                repository.Update(league);
                return RedirectToAction("Index");
            }
            return View("Update");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var league = repository.GetById(id);
            if (league == null)
            {
                return NotFound();
            }
            repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
