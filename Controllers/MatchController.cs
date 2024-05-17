using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Tazaker.Models;
using Tazkara.Data;
using Tazkara.IRepository;
using Tazkara.ViewModels;

namespace Tazkara.Controllers
{
    public class MatchController : Controller
    {
        IMatch repository;
        private readonly ApplicationDbContext context;

        public MatchController(IMatch repository, ApplicationDbContext context)
        {
            this.repository = repository;
            this.context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Matches = repository.GetAll();

            Dictionary<string, string> dic = new Dictionary<string, string>();
            int i = 1;

            foreach (var item in Matches)
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
                repository.Delete(match.Id);
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
