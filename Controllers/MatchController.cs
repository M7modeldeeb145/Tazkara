using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Tazaker.Models;
using Tazkara.IRepository;
using Tazkara.ViewModels;

namespace Tazkara.Controllers
{
    public class MatchController : Controller
    {
        IMatch repository;
        public MatchController(IMatch repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Matches = repository.GetAll();
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
            };
            return View(matchviewmodel);
        }
        [HttpPost]
        public IActionResult Update(MatchViewModel matchviewmodel)
        {
            if (ModelState.IsValid)
            {
                var match = repository.GetById(matchviewmodel.Id);
                repository.Update(match);
                return RedirectToAction("Index");
            }
            return View("Update");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var match = repository.GetById(id);
            if (match != null)
            {
                repository.Delete(id);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult Search (string name)
        {
            var matches = repository.Search(name);
            return View(matches);
        }
    }
}
