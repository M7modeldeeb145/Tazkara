using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tazkara.ViewModels;

namespace Tazkara.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return View(roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRoleVM userRoleVM)
        {
            if (!ModelState.IsValid)
                return View("Index", await roleManager.Roles.ToListAsync());
            if(await roleManager.RoleExistsAsync(userRoleVM.Name)) 
            {
                ModelState.AddModelError("Name", "Role Is Exists");
                return View("Index", await roleManager.Roles.ToListAsync());
            }
            await roleManager.CreateAsync(new IdentityRole(userRoleVM.Name.Trim()));
            return RedirectToAction("Index");
        }
    }
}
