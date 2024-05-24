using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tazaker.Models;
using Tazkara.IRepository;

namespace Tazkara.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactUsController : Controller
    {
        IContactUs repository;
        public ContactUsController(IContactUs repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            var contactus = repository.ReadAll();
            return View(contactus);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Create(ContactUs contactus)
        {
            if (ModelState.IsValid)
            {
                var con = new ContactUs()
                {
                    Email = contactus.Email,
                    Id = contactus.Id,
                    Name = contactus.Name,
                    Message = contactus.Message,
                    Status = contactus.Status,
                    Subject = contactus.Subject,
                };
                repository.Create(con);
                return RedirectToAction("Index");
            }
            return View("Error");
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View(new ContactUs());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ContactUs contactus)
        {
            if (ModelState.IsValid)
            {
                var con = repository.GetById(contactus.Id);
                if (con != null)
                {
                    repository.Update(con);
                    return RedirectToAction("Index");
                }
            }
            return View("Error");
        }
        public IActionResult Delete(int id)
        {
            var delete = repository.GetById(id);
            if (delete != null)
            {
                repository.Delete(id);
                return RedirectToAction("Index");
            }
            return View("Error");
        }
    }
}
