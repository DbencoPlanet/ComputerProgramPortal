using ComProgramPortal.Areas.Data.IService;
using ComProgramPortal.Areas.Data.Service;
using ComProgramPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ComProgramPortal.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin,SuperAdmin,Student")]
    [AllowAnonymous]
    public class StudentProfilesController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private IStudentInfoService _studentProfileService = new StudentInfoService();




        public StudentProfilesController()
        {

        }
        public StudentProfilesController(StudentInfoService studentProfileService)
        {
            _studentProfileService = studentProfileService;
        }

        // GET: Admin/StudentProfile
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        //public async Task<ActionResult> Profile (string id)
        public async Task<ActionResult> StudentProfile(string id)
        {
            var myid = await db.StudentInfos.Include(x => x.user).FirstOrDefaultAsync(x => x.UserId == id);
            return RedirectToAction("StudentProfile", new { id = myid });
        }
        [AllowAnonymous]
        public async Task<ActionResult> StudentProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var student = await _studentProfileService.Get(id);
            var currentpackage = await _studentProfileService.PackageNameForStudent();
            ViewBag.currentpackage = currentpackage;
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [AllowAnonymous]
        public async Task<ActionResult> StudentProfile2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var student = await _studentProfileService.Get(id);
            var currentpackage = await _studentProfileService.PackageNameForStudent();
            ViewBag.currentpackage = currentpackage;
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
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