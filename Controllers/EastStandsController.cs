using Microsoft.AspNetCore.Mvc;
using Tazkara.IRepository;

namespace Tazkara.Controllers
{
    public class EastStandsController : Controller
    {
        IEastStands repository;
        public EastStandsController(IEastStands repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Seets = repository.GetAll();
            return View(Seets);
        }
    }
}
