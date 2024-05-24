using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tazaker.Models;
using Tazkara.IRepository;
using Tazkara.ViewModels;

namespace Tazkara.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        ITicket repository;
        public TicketController(ITicket repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var tickets = repository.GetAll();
            return View(tickets);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Stadiums"] = repository.GetStadiums();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TicketViewModel ticketViewModel,int id)
        {
            if (ModelState.IsValid)
            {
                var match = repository.GetMatch(id);
                var ticket = new Ticket()
                {
                    Id = ticketViewModel.Id,
                    MatchId = ticketViewModel.MatchId,
                    ReferenceNum = new Guid(),
                    StadiumId = ticketViewModel.StadiumId,
                    Title = ticketViewModel.Title,
                };
                repository.Create(ticket);
                return RedirectToAction("Index",match);
            }
            return View("Create");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var ticket = repository.GetById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            var TicketVM = new TicketViewModel()
            {
                Id = ticket.Id,
                MatchId = ticket.MatchId,
                ReferenceNum = ticket.ReferenceNum,
                StadiumId = ticket.StadiumId,
                Title = ticket.Title,
            };
            return View(TicketVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(TicketViewModel ticketViewModel)
        {
            if (ModelState.IsValid)
            {
                var ticket = repository.GetById(ticketViewModel.Id);
                if (ticket == null)
                {
                    return NotFound();
                }
                repository.Update(ticket);
                return RedirectToAction("Index");
            }
            return View("Update");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var ticket = repository.GetById(id);
            if (ticket != null)
            {
                repository.Delete(id);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
