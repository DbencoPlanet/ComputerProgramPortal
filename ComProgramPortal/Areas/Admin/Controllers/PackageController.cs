using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ComProgramPortal.Models;
using ComProgramPortal.Models.Entities;
using ComProgramPortal.Areas.Data.IService;
using ComProgramPortal.Areas.Data.Service;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using PagedList;

namespace ComProgramPortal.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class PackageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IUserManagerService _userService = new UserManagerService();
        private IPackageService _packageService = new PackageService();
        private ITeacherProfileService _teacherService = new TeacherProfileService();
        private ImageService _imageService = new ImageService();




        public PackageController()
        {

        }
        public PackageController(UserManagerService userService, ApplicationUserManager userManager, ApplicationRoleManager roleManager, StudentInfoService studentProfile, PackageService packageService,TeacherProfileService teacherService,ImageService imageService)
        {
            _userService = userService;
            UserManager = userManager;
            _teacherService = teacherService;
            //_classlevelService = classLevelService;
            RoleManager = roleManager;
            _packageService = packageService;
            _imageService = imageService;


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


        // GET: Admin/Package
        //public ActionResult Index()
        //{
        //    var packages = db.Packages.Include(p => p.User);
        //    return View(packages.ToList());
        //}

        public async Task<ActionResult> Index(string searchString, string currentFilter, int? page)
        {

            var user = User.Identity;
            //ViewBag.Roles = db.Roles.Where(x => x.Name != "SuperAdmin").ToList();
            ViewBag.Roles = RoleManager.Roles.Where(x => x.Name != "SuperAdmin").ToList();



            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var items = await _packageService.PackageList(searchString, currentFilter, page);
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));

        }



        // GET: Admin/Package/Students/5
        public async Task<ActionResult> Students(int? id)
        {
            var items = await _packageService.Students(id);

            return View(items);
        }



        // GET: Admin/Package/StudentsCounts/5
        public async Task<ActionResult> StudentsCount(int? id)
        {
            var items = await _packageService.StudentsCount(id);
           
            return View(items);
        }


        // GET: Admin/Package/PackageDetails/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    var items = await _packageService.PackageDetails(id);
        //    ViewBag.Package = items;
        //    return View(items);
        //}



        //// GET: Admin/Package/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }



        // GET: Admin/Payment/Create
        public ActionResult Create()
        {
            //ViewBag.StudentName = new SelectList(db.StudentInfos, "Id", "Surname");
            //ViewBag.TeacherName = new SelectList(db.TeacherProfiles, "Id", "Fullname");
            ViewBag.TeacherName = new SelectList(db.TeacherProfiles.Include(x => x.user), "UserId", "Fullname");
            return View();
        }

        // POST: Admin/Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,UserId,PackageName,Duration,StartDate,EndDate,PackageAmount,PackageInfo")] Package package, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var image = await _imageService.Create(upload);
                db.Packages.Add(package);
                TempData["success"] = "Package has been Added Successfully";
                await db.SaveChangesAsync();
                TempData["failure"] = "Package was not created";
                return RedirectToAction("Create");
            }

            //ViewBag.TeacherName = new SelectList(db.TeacherProfiles, "Id", "Fullname", package.UserId);
            ViewBag.TeacherName = new SelectList(db.TeacherProfiles.Include(x => x.user), "UserId", "Fullname", package.UserId);
            return View(package);
        }


        //POST: Admin/Package/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Teacher,PackageName,Duration,StartDate,EndDate,PackageAmount,PackageInfo")] Package package)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Packages.Add(package);
        //        db.SaveChanges();

        //        TempData["success"] = "Package has been Added Successfully.";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        if (ModelState.IsValid == false)
        //        {
        //            TempData["failure"] = "Package was not added.";
        //            return RedirectToAction("Create");
        //        }
        //    }



        //    ViewBag.UserId = new SelectList(db.TeacherProfiles.Include(x => x.user), "Id", "Fullname", package.Teacher);
        //    return View(ViewBag.UserId);
        //}


        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var item = await _packageService.Get(id);
        //    if (item == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var teacher = await _teacherService.TeacherDropdownList();
        //    ViewBag.UserId = new SelectList(teacher, "UserId", "FullName");
        //    return View(item);
        //}


        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(Package model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        await _packageService.Edit(model);
        //        return RedirectToAction("Index");
        //    }
        //    var staff = await _teacherService.TeacherDropdownList();
        //    ViewBag.UserId = new SelectList(staff, "UserId", "FullName", model.UserId);
        //    return View(model);
        //}



        //// GET: Admin/Package/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeacherName = new SelectList(db.TeacherProfiles.Include(x=>x.user), "UserId", "Fullname");
            //ViewBag.PackageName = new SelectList(db.Packages, "Id", "PackageName");
            return View(package);
        }

        // POST: Admin/Package/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,PackageName,Duration,StartDate,EndDate,PackageAmount,PackageInfo")] Package package)
        {
            if (ModelState.IsValid)
            {
                db.Entry(package).State = EntityState.Modified;
                TempData["success"] = "Package has been Updated Successfully";
                db.SaveChanges();
                TempData["failure"] = "Package was not updated try again";
                return RedirectToAction("Index");
            }
            ViewBag.TeacherName = new SelectList(db.TeacherProfiles.Include(x => x.user), "UserId", "Fullname", package.UserId);
            //ViewBag.PackageName = new SelectList(db.Packages, "Id", "PackageName");
            return View(package);
        }

        // GET: Admin/Package/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // POST: Admin/Package/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Package package = db.Packages.Find(id);
            db.Packages.Remove(package);
            db.SaveChanges();
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
