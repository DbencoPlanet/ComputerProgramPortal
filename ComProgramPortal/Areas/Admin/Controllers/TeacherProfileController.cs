using ComProgramPortal.Areas.Data.IService;
using ComProgramPortal.Areas.Data.Service;
using ComProgramPortal.Models;
using ComProgramPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SchoolPortal.Web.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin,SuperAdmin,Teacher")]
    [AllowAnonymous]
    public class TeacherProfileController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private ITeacherProfileService _teacherProfileService = new TeacherProfileService();


        public TeacherProfileController()
        {

        }
        public TeacherProfileController(TeacherProfileService teacherProfileService)
        {
            _teacherProfileService = teacherProfileService;
        }
        // GET: Admin/StaffProfile
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> TeacherProfile(int? id)
        {
            try
            {
                var profile = await _teacherProfileService.Get(id);
                ViewBag.teacher = profile;


                //var package = await _teacherProfileService.TeacherPackageName();
                //ViewBag.Pack = package;

                //var qualification = await _teacherProfileService.ListQualificationForUser();
                //ViewBag.Qualification = qualification;

            }
            catch (Exception e)
            {

            }
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = await _teacherProfileService.Get(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }


        public async Task<ActionResult> MyQualification(int id)
        {
            var items = await _teacherProfileService.ListQualification(id);
            return View(items);
        }


        public async Task<ActionResult> QualificationDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = await _teacherProfileService.GetQualification(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }


        public ActionResult AddQualification()
        {

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddQualification(TeacherQualification model)
        {
            if (ModelState.IsValid)
            {
                var id = await _teacherProfileService.CreateQualification(model);
                TempData["success"] = "Qualification Added Successfully";
                return RedirectToAction("Index", "Panel", new { area = "Teacher" });
            }
            TempData["error"] = "Unable to add Qualification";
            return View(model);
        }

        public async Task<ActionResult> EditQualification(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = await _teacherProfileService.GetQualification(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }


        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditQualification(TeacherQualification model)
        {
            if (ModelState.IsValid)
            {
                var id = await _teacherProfileService.EditQualification(model);
                TempData["success"] = "Qualification Updated Successfully";
                return RedirectToAction("Index", "Panel", new { area = "Teacher" });
            }
            TempData["error"] = "Unable to edit Qualification";

            return View(model);
        }

        public async Task<ActionResult> DeleteQualification(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = await _teacherProfileService.GetQualification(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Admin/Settings/Delete/5
        [HttpPost, ActionName("DeleteQualification")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteQualificationConfirmed(int id)
        {
            var sid = await _teacherProfileService.DeleteQualification(id);
            TempData["success"] = "Qualification Deleted Successfully";
            return RedirectToAction("Index", "Panel", new { area = "Teacher" });
        }



    }
}