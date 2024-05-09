using Microsoft.AspNetCore.Mvc;
using Tazaker.Models;
using Tazkara.IRepository;
using Tazkara.ViewModels;

namespace Tazkara.Controllers
{
    public class TeamController : Controller
    {
        ITeam repository;
        public TeamController(ITeam repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var teams = repository.GetAll();
            return View(teams);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (TeamViewModel teamViewModel)
        {
            if (!ModelState.IsValid)
            {
                var team = new Team()
                {
                    Name = teamViewModel.Name,
                    TeamLogo = teamViewModel.TeamLogo,
                    MatchId = teamViewModel.MatchId,
                    Id = teamViewModel.Id
                };
                repository.Create(team);
                return RedirectToAction("Index");
            }
            return View("Create");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var team = repository.GetById(id);
            if (team == null)
            {
                return RedirectToAction("Index");
            }
            var teamVM = new TeamViewModel()
            {
                MatchId = team.MatchId,
                Name = team.Name,
                TeamLogo = team.TeamLogo,
            };
            return View(teamVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(TeamViewModel teamViewModel)
        {
            if (!ModelState.IsValid)
            {
                var team = repository.GetById(teamViewModel.Id);
                repository.Update(team);
                return RedirectToAction("Index");
            }
            return View("Update");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var team = repository.GetById(id);
            if (team == null)
            {
                return NotFound();
            }
            repository.Delete(id); 
            return RedirectToAction("Index");
        }
    }
}
