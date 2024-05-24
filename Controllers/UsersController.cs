using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using Tazkara.Models;
using Tazkara.ViewModels;

namespace Tazkara.Controllers
{
    [Authorize (Roles ="Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly RoleManager<IdentityRole> rolemanager;
        public UsersController(UserManager<ApplicationUser> usermanager, RoleManager<IdentityRole> rolemanager)
        {
            this.usermanager = usermanager;
            this.rolemanager = rolemanager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await usermanager.Users.Select(user => new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Roles = usermanager.GetRolesAsync(user).Result
            }).ToListAsync();
            return View(users);
        }
        public async Task<IActionResult> Create()
        {
            var roles = await rolemanager.Roles.Select(role => new RoleViewModel() { RoleId = role.Id,RoleName = role.Name}).ToListAsync();
            var VM = new AddUserViewModel()
            {
                Roles = roles
            };
            return View(VM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            if(!model.Roles.Any(r=>r.IsSelected))
            {
                ModelState.AddModelError("Roles","Please Select At Least One Role");
                return View(model);
            }
            if(await usermanager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email","Email Is Already Exists");
                return View(model);
            }
            if(await usermanager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("Username","Username Is Already Exists");
                return View(model);
            }
            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var result = await usermanager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Roles", error.Description);
                }
                return View(model);
            }
            await usermanager.AddToRolesAsync(user, model.Roles.Where(r=>r.IsSelected).Select(r=>r.RoleName));
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(string userId)
        {
            var user = await usermanager.FindByIdAsync(userId);
            if(user == null)
                return NotFound();
            var viewmodel = new ProfileFormViewModel()
            {
                Id = userId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            };
            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProfileFormViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);
            var user = await usermanager.FindByIdAsync(model.Id);
            if (user == null)
                return NotFound();
            var userwithsameemail = await usermanager.FindByEmailAsync(model.Email);
            if (userwithsameemail != null && userwithsameemail.Id != model.Id)
            {
                ModelState.AddModelError("Email", "This Email Is Assigned to Another User");
                return View(model);
            }
            var userwithsameusername = await usermanager.FindByNameAsync(model.UserName);
            if (userwithsameusername != null && userwithsameusername.Id != model.Id)
            {
                ModelState.AddModelError("UserName", "This UserName Is Assigned to Another User");
                return View(model);
            }
            user.UserName = model.UserName;
            user.LastName = model.LastName;
            user.FirstName = model.FirstName;
            user.Email = model.Email;
            await usermanager.UpdateAsync(user);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ManageRoles(string userid)
        {
            var user = await usermanager.FindByIdAsync(userid);
            if (user == null)
            {
                return NotFound();
            }
            var roled = await rolemanager.Roles.ToListAsync();
            var viewmodel = new UserRolesViewModel 
            {
                 UserId = user.Id,
                 UserName= user.UserName,
                 Roles = roled.Select(r => new RoleViewModel()
                 {
                     RoleId = r.Id,
                     RoleName = r.Name,
                     IsSelected = usermanager.IsInRoleAsync(user,r.Name).Result
                 }).ToList(),
            };
            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesViewModel model)
        {
			var user = await usermanager.FindByIdAsync(model.UserId);
			if (user == null)
			{
				return NotFound();
			}
            var userroles = await usermanager.GetRolesAsync(user);
            foreach (var role in model.Roles)
            {
                if(userroles.Any(r=>r == role.RoleName)&& !role.IsSelected)
                    await usermanager.RemoveFromRoleAsync(user,role.RoleName);

                if(!userroles.Any(r=>r == role.RoleName)&& role.IsSelected)
                    await usermanager.AddToRoleAsync(user,role.RoleName);
            }
			return RedirectToAction("Index");
		}
        public async Task<IActionResult> Delete(string userId)
        {
            var user = await usermanager.FindByIdAsync (userId);    
            if (user == null)
                return NotFound();
             
            var result = await usermanager.DeleteAsync (user);
            if (!result.Succeeded)
                return NotFound();
            return RedirectToAction("Index");
        }
    }
}
