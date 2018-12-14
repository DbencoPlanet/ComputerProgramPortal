using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data.Entity;
using System.Data;
using ComProgramPortal.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ComProgramPortal.Areas.SuperAdmin.Controllers
{
    public class SuperAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public SuperAdminController()
        {
        }

        public SuperAdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult CreateAccount()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAccount(RegisterViewModel model)
        {
            model.Username = "SuperAdmin";
            model.Email = "superadmin@super.com";
            var user = new ApplicationUser { UserName = model.Username, Email = model.Email, DateRegistered = DateTime.UtcNow.AddHours(1) };
            model.Password = "super@123";

            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var role = new IdentityRole("SuperAdmin");
                var role1 = new IdentityRole("Teacher");
                var role2 = new IdentityRole("Student");
                var role3 = new IdentityRole("Admin");
                var role4 = new IdentityRole("Mods");
                await RoleManager.CreateAsync(role);
                await RoleManager.CreateAsync(role1);
                await RoleManager.CreateAsync(role2);
                await RoleManager.CreateAsync(role3);
                await RoleManager.CreateAsync(role4);

                await UserManager.AddToRoleAsync(user.Id, "SuperAdmin");
                ////
                ///

                ///
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                return RedirectToAction("CreateAdmin");
            }
            TempData["error"] = "Contact Administrator";
            return View();

        }


        public async Task<ActionResult> CreateAdmin(RegisterViewModel model)
        {
            model.Username = "Admin";
            model.Email = "admin@admin.com";
            var user = new ApplicationUser { UserName = model.Username, Email = model.Email, DateRegistered = DateTime.UtcNow.AddHours(1) };
            model.Password = "Admin@123";

            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                await UserManager.AddToRoleAsync(user.Id, "Admin");
                ////
                ///

                return RedirectToAction("Index", "DashBoard", new { area = "Admin" });
            }
            return View();

        }
    }
}