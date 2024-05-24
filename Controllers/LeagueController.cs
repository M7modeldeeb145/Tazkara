using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tazaker.Models;
using Tazkara.Data;
using Tazkara.IRepository;
using Tazkara.ViewModels;

namespace Tazkara.Controllers
{
    [Authorize (Roles ="Admin")]
    public class LeagueController : Controller
    {
        ILeague repository;
        private readonly ApplicationDbContext context;
        public LeagueController(ILeague repository,ApplicationDbContext context)
        {
            this.context = context;
            this.repository = repository;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            var leagues = repository.GetAll();
            return View(leagues);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ShowTeams(int id)
        {
            var Matches = repository.GetMatchesInLeague(id);
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
                    LeagueLogo = leagueViewModel.LeagueLogo
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
                LeagueLogo = league.LeagueLogo
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
                league.Name = leagueViewModel.Name;
                league.LeagueLogo = leagueViewModel.LeagueLogo;
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
