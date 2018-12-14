using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using ComProgramPortal.Areas.Data.IService;
using ComProgramPortal.Areas.Data.Service;
using ComProgramPortal.Models;
using ComProgramPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
//using static ComProgramPortal.Models.Entities.AdminViewModel;

namespace ComProgramPortal.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class UserManagersController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        //private IClassLevelService _classlevelService = new ClassLevelService();
        private IEnrollmentService _enrollmentService = new EnrollmentService();
        private IUserManagerService _userService = new UserManagerService();
        private IImageService _imageService = new ImageService();
        private IStudentInfoService _studentService = new StudentInfoService();
        private ITeacherProfileService _teacherService = new TeacherProfileService();
       

        public UserManagersController()
        {

        }
        public UserManagersController(UserManagerService userService, ApplicationUserManager userManager, ApplicationRoleManager roleManager, EnrollmentService enrollmentService, ImageService imageService, StudentInfoService studentProfile,TeacherProfileService teacherService)
        {
            _userService = userService;
            UserManager = userManager;
            //_classlevelService = classLevelService;
            RoleManager = roleManager;
            _imageService = imageService;
            _studentService = studentProfile;
            _enrollmentService = enrollmentService;
            _teacherService = teacherService;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
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

        // Swap ApplicationRole for IdentityRole:




        [HttpPost]
        public async Task<ActionResult> UpdateSurname(string stdid, string surname)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == stdid);
            user.Surname = surname;
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return new EmptyResult();

        }
        // GET: Admin/UserManagers
        public async Task<ActionResult> Index(string searchString, string currentFilter, int? page)
        {
            ViewBag.Roles = RoleManager.Roles.Where(x => x.Name != "SuperAdmin").ToList();

            //var user = User.Identity;
            //ViewBag.Roles = db.Roles.Where(x => x.Name != "SuperAdmin").ToList();


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var items = await _userService.AllUsers(searchString, currentFilter, page);
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));

        }

        public async Task<ActionResult> AllTeacher(string searchString, string currentFilter, int? page)
        {
            ViewBag.Roles = RoleManager.Roles.Where(x => x.Name != "SuperAdmin").ToList();

            //ViewBag.Roles = db.Roles.Where(x => x.Name != "SuperAdmin").ToList();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var items = await _userService.ListTeacher(searchString, currentFilter, page);
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));

        }

        public async Task<ActionResult> Teacher(string searchString, string currentFilter, int? page)
        {

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var items = await _userService.ListTeacher(searchString, currentFilter, page);
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));

        }

        public async Task<ActionResult> AllStudents(string searchString, string currentFilter, int? page)
        {
            //ViewBag.Roles = RoleManager.Roles.Where(x => x.Name != "SuperAdmin").ToList();
            ViewBag.Roles = db.Roles.Where(x => x.Name != "SuperAdmin").ToList();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var items = await _userService.ListStudent(searchString, currentFilter, page);
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));

        }

        public async Task<ActionResult> Students(string searchString, string currentFilter, int? page)
        {

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var items = await _userService.ListStudent(searchString, currentFilter, page);
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));

        }



        [AllowAnonymous]
        public async Task<ActionResult> StudentProfile(int? id, string studentID)
        {
            try
            {
                var profile = await _studentService.Get(id);
                ViewBag.student = profile;

                var currentpackage = await _studentService.PackageNameForStudent2(studentID);
                ViewBag.currentpackage = currentpackage;


            }
            catch (Exception e)
            {

            }
            return View();
        }


        //[AllowAnonymous]
        ////public async Task<ActionResult> Profile (string id)
        //public async Task<ActionResult> StudentProfile(string id)
        //{
        //    var myid = await db.StudentInfos.Include(x => x.user).FirstOrDefaultAsync(x => x.UserId == id);
        //    return RedirectToAction("StudentProfile", new { id = myid });
        //}
        //[AllowAnonymous]
        //public async Task<ActionResult> StudentProfile(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var student = await _studentService.Get(id);
        //    var currentpackage = await _studentService.PackageNameForStudent();
        //    ViewBag.currentpackage = currentpackage;
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}



        [AllowAnonymous]
        public async Task<ActionResult> TeacherProfile(int? id)
        {
            try
            {
                var profile = await _teacherService.Get(id);
                ViewBag.teacher = profile;

            }
            catch (Exception e)
            {

            }
            return View();
        }


        // GET: Admin/UserManagerss/Create
        public ActionResult NewTeacher()
        {
            ViewBag.StateName = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName");
            return View();
        }
        static string ConvertStringArrayToString(string[] array)
        {
            // Concatenate all the elements into a StringBuilder.
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append('.');
            }
            return builder.ToString();
        }
        // POST: Admin/UserManagers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewTeacher(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string succed;
                succed = await _userService.NewTeacher(model);
                if (succed == "true")
                {
                    TempData["success"] = "Teacher with username <i> " + model.Username + "</i> Added Successfully.";
                    return RedirectToAction("NewTeacher");
                }
                else
                {


                    TempData["error1"] = succed;
                }

            }
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            TempData["error"] = "Creation of new teacher not successful";
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName", model.StateOfOrigin);
            return View(model);
        }

        public ActionResult NewStudent()
        {
            //var classlevel = await _classlevelService.ClassLevelList();
            ViewBag.PackageId = new SelectList(db.Packages, "Id", "PackageName");
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName");
            return View();
        }

        // POST: Admin/Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewStudent(RegisterViewModel model, HttpPostedFileBase upload, int packageId)
        {
            var ee = "";
            if (ModelState.IsValid)
            {



                try
                {
                    string succed;

                    succed = await _userService.NewStudent(model);
                    if (succed == "true")
                    {
                        var Imageid = await _imageService.Create(upload);
                        var user = await UserManager.FindByNameAsync(model.Username);
                        // var user = await _userService.GetUserByUserEmail(model.Email);
                        var student = await _studentService.GetStudentByUserId(user.Id);

                        //profile pic upload
                        await _studentService.UpdateImageId(student.Id, Imageid);

                        //enrolment
                        //await _enrollmentService.EnrollStudent(classId, student.Id);
                        //var classLevel = await _classlevelService.Get(classId);
                        TempData["success"] = "Student with username <i> " + model.Username + "</i> Added Successfully";
                        return RedirectToAction("NewStudent");
                    }
                    else
                    {
                        TempData["error1"] = succed;
                    }


                }
                catch (Exception e)
                {
                    ee = e.ToString();
                }

            }
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            TempData["error"] = "Creation of new student not successful" + ee;
            //var classlevel = await _classlevelService.ClassLevelList();
            ViewBag.PackageId = new SelectList(db.Packages, "Id", "PackageName");
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName", model.StateOfOrigin);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> UserToRole(string rolename, string userId, bool? ischecked)
        {
            if (ischecked.HasValue && ischecked.Value)
            {
                await _userService.AddUserToRole(userId, rolename);
            }
            else
            {
                await _userService.RemoveUserToRole(userId, rolename);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Roles/
        public ActionResult Roles()
        {

            return View(RoleManager.Roles.Where(x => x.Name != "SuperAdmin"));
            //return View(db.Roles.Where(x => x.Name != "SuperAdmin"));
        }

        //s
        // GET: /Roles/Details/5
        public async Task<ActionResult> RoleDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            // Get the list of Users in this Role
            var users = new List<ApplicationUser>();

            // Get the list of Users in this Role
            var usersitem = await _userService.Users();
            foreach (var user in usersitem)
            {
                if (await _userService.IsUsersinRole(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();
            return View(role);
        }


        // GET: /Roles/Create
        public ActionResult CreateRole()
        {
            return View();
        }


        // POST: /Roles/Create
        [HttpPost]
        public async Task<ActionResult> CreateRole(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole(roleViewModel.Name);
                var roleresult = await RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First());
                    return View();
                }
                return RedirectToAction("Roles");
            }
            return View();
        }


       

        //
        // GET: /Roles/Edit/Admin
        public async Task<ActionResult> EditRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ////var role = await RoleManager.FindByIdAsync(id);
            var role = await RoleManager.FindByIdAsync(id);
            //var role = db.Roles.Find(r => r.Name == id);

            if (role == null)
            {
                return HttpNotFound();
            }
            RoleViewModel roleModel = new RoleViewModel { Id = role.Id, Name = role.Name };
            return View(roleModel);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRole(RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(roleModel.Id);
                role.Name = roleModel.Name;
                await RoleManager.UpdateAsync(role);
                return RedirectToAction("Roles");
            }
            return View();
        }

        //
        // GET: /Roles/Delete/5
        public async Task<ActionResult> DeleteRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Roles/Delete/5
        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id, string deleteUser)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var role = await RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                IdentityResult result;
                if (deleteUser != null)
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                else
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Roles");
            }
            return View();
        }

        public JsonResult LgaList(string Id)
        {
            var stateId = db.States.FirstOrDefault(x => x.StateName == Id).Id;
            var LGA = from s in db.LocalGovs
                        where s.StatesId == stateId
                        select s;

            return Json(new SelectList(LGA.ToArray(), "LGAName", "LGAName"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// change student status via expelled ....
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>



        ////end

        /// <summary>
        /// change staff status via expelled ....
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

    
     
        ////end



        public async Task<ActionResult> Edit(string id, string ReturnUrl)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.StateName = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName");
            var user = await _userService.GetUserByUserId(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View(user);
        }

        // POST: Admin/Settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ApplicationUser model, string ReturnUrl)
        {
            if (model.Id != null)
            {
                try
                {
                    bool check = await _userService.UpdateUser(model);
                    if (check == true)
                    {
                        TempData["success"] = "User Updated Successfully";
                        if (ReturnUrl == null)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Redirect(ReturnUrl);
                        }

                    }
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }


            }
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName", model.StateOfOrigin);
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            TempData["error"] = "Update not successful";
            ViewBag.ReturnUrl = ReturnUrl;
            return View(model);
        }


        public async Task<ActionResult> Islocked(string UserId)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == UserId);
            if (user.IsLocked == true)
            {
                user.IsLocked = false;
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();

            }
            else
            {
                user.IsLocked = true;
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}