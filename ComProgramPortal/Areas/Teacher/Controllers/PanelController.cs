using ComProgramPortal.Areas.Data.IService;
using ComProgramPortal.Areas.Data.Service;
using ComProgramPortal.Models;
using ComProgramPortal.Models.Entities;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ComProgramPortal.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher,Admin,SuperAdmin")]
    public class PanelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ITeacherProfileService _teacherProfileService = new TeacherProfileService();
        private IPackageService _packageService = new PackageService();
        private IEnrollmentService _enrollmentService = new EnrollmentService();
        private IStudentInfoService _studentInfoService = new StudentInfoService();
        private IImageService _imageService = new ImageService();
        private IAssignmentService _assignmentService = new AssignmentService();


        public PanelController()
        {

        }
        public PanelController(
            TeacherProfileService teacherProfileService,
            PackageService packageService,
            EnrollmentService enrollmentService,
            StudentInfoService studentInfoService,
            ImageService imageService,
            AssignmentService assignmentService
            )
        {
            _imageService = imageService;
            _teacherProfileService = teacherProfileService;
            _packageService = packageService;
            _enrollmentService = enrollmentService;
            _studentInfoService = studentInfoService;
            _assignmentService = assignmentService;
        }
        // GET: Staff/Panel
        [Authorize(Roles = "SuperAdmin,Admin,Teacher")]
        public async Task<ActionResult> Index()
        {
            try
            {
                var profile = await _teacherProfileService.GetTeacherWithoutId();
                ViewBag.teacher = profile;


                var package = await _teacherProfileService.TeacherPackageName();
                ViewBag.Pack = package;

                var qualification = await _teacherProfileService.ListQualificationForUser();
                ViewBag.Qualification = qualification;

            }
            catch (Exception e)
            {
          
            }
            return View();
        }


        public async Task<ActionResult> TeacherProfile()
        {
            try
            {
                var profile = await _teacherProfileService.GetTeacherWithoutId();
                ViewBag.teacher = profile;


                var package = await _teacherProfileService.TeacherPackageName();
                ViewBag.Pack = package;

                var qualification = await _teacherProfileService.ListQualificationForUser();
                ViewBag.Qualification = qualification;

            }
            catch (Exception e)
            {

            }
            return View();
        }


        [AllowAnonymous]
        public async Task<ActionResult> TeacherProfile2(int? id)
        {
            try
            {
                var profile = await _teacherProfileService.Get(id);
                ViewBag.teacher = profile;


                var package = await _teacherProfileService.TeacherPackageName();
                ViewBag.Pack = package;

                var qualification = await _teacherProfileService.ListQualificationForUser();
                ViewBag.Qualification = qualification;

            }
            catch (Exception e)
            {

            }
            return View();
        }
        #region
        // GET: Teacher/Assignments/Create
        public ActionResult AddAssignment(int? packageId)
        {
            var userid = HttpContext.User.Identity.GetUserId();

            ViewBag.PackageId = new SelectList(db.Packages, "Id", "PackageName");
            return View();
        }

        // POST: Teacher/Assignments/Creates
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAssignment([Bind(Include = "Id,PackageId,UserId,Title,Description,DateCreated,DateSubmitionEnds,IsPublished")]Assignment assignment,int? packageId,string userid)
        {
            if (ModelState.IsValid)
            {
                userid = HttpContext.User.Identity.GetUserId();
                assignment.IsPublished = false;
                assignment.DateCreated = DateTime.UtcNow.AddHours(1);
                assignment.PackageId = ViewBag.PackageId;
                assignment.UserId = userid;
                //assignment.Id = ViewBag.PackageId;
                db.Assignments.Add(assignment);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("AssignmentList", new { packageId = packageId});
            }

            ViewBag.PackageId = new SelectList(db.Packages, "Id", "PackageName", assignment.PackageId);
            return View(assignment);
        }


        public async Task<ActionResult> AssignmentList(int? packageId)
        {
            if (packageId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var userid = User.Identity.GetUserId();
            //var teacherId = await db.TeacherProfiles.Include(x => x.user).FirstOrDefaultAsync(x => x.UserId == userid);
            //var assignment = db.Assignments.Include(x => x.Package).Include(x => x.User).Where(x => x.Id == teacherId.Id && x.PackageId == packageId).ToListAsync();
            var assignment = await db.Assignments.Include(x => x.Package).Include(x => x.User).Where(x => x.Id == packageId).ToListAsync();
            var c = await db.Packages.FirstOrDefaultAsync(x => x.Id == packageId);
            ViewBag.c = c.PackageName;
            ViewBag.packageId = packageId;
            //ViewBag.user = userid;
            return View(assignment);
        }

        public async Task<ActionResult> Assignment(int? packageId)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var teacherId = await db.TeacherProfiles.Include(x => x.user).FirstOrDefaultAsync(x => x.UserId == userid);
                var myass = db.Assignments.Include(x => x.Package).Include(x => x.AssignmentAnswers).Include(x => x.User).Where(x=>x.PackageId == teacherId.Id).OrderByDescending(x =>x.DateCreated).ToList();
                //var c = await db.Assignments.Include(x=>x.Package).FirstOrDefaultAsync(x => x.PackageId == packageId);
                //ViewBag.c = c.Package.PackageName;
                return View(myass);
            }
            catch (Exception ex)
            {
                return RedirectToAction("AssignmentIndex");
            }

        }


        public async Task<ActionResult> Assignment2(int? packageId)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var teacherId = await db.TeacherProfiles.Include(x => x.user).FirstOrDefaultAsync(x => x.UserId == userid);
                var myass = db.Assignments.Include(x => x.Package).Include(x => x.AssignmentAnswers).Include(x => x.User).Where(x => x.PackageId == teacherId.Id).ToList();

                return View(myass);
            }
            catch (Exception ex)
            {
                return RedirectToAction("AssignmentIndex");
            }

        }
        // GET: /Assignment/Assignment/
        public async Task <ActionResult> AssignmentIndex()
        {
            var userid = User.Identity.GetUserId();
            var teacherId = await db.TeacherProfiles.Include(x => x.user).FirstOrDefaultAsync(x => x.UserId == userid);
            var my = db.Assignments.Include(x => x.Package).Include(x => x.AssignmentAnswers).Include(x => x.User).Where(x => x.PackageId == teacherId.Id).OrderBy(x => x.DateCreated == DateTime.Today).ToList();
            ViewBag.Count = my.Count;
            return View();
        }


        public async Task<ActionResult> PublishOrUnpublish(int id, int? packageId)
        {
            var check = await _assignmentService.Get(id);
            if (check.IsPublished == true)
            {
                check.IsPublished = false;
            }
            else
            {
                check.IsPublished = true;
            }
            await _assignmentService.Edit(check);
            return RedirectToAction("Assignment", new { packageId = packageId});
        }


        public async Task<ActionResult> AssessOrUnassess(int? id)
        {
            //var assignment = await db.AssignmentAnswers.Include(x => x.Package).Include(x => x.Enrollement).Include(x => x.Assignment).Include(x => x.User).Include(x => x.StudentInfo).Where(x => x.AssignmentId == id && x.StudentId == studentId).ToListAsync();
            var check = await _assignmentService.Get(id);
            foreach(var ass in check.AssignmentAnswers)
            {
                if (ass.Assessed == true)
                {
                    ass.Assessed = false;
                }
                else
                {
                    ass.Assessed = true;
                }
            }
           
            await _assignmentService.Edit(check);
            return RedirectToAction("Assignment");
        }


        

        public async Task<ActionResult> AnsweredAssignment(int? id, int? studentId)
        {

            var assignment = await db.AssignmentAnswers.Include(x => x.Package).Include(x => x.Enrollement).Include(x => x.Assignment).Include(x=>x.User).Include(x => x.StudentInfo).Where(x => x.AssignmentId == id && x.StudentId == studentId).OrderByDescending(x=>x.DateAnswered).ToListAsync();
            var item = await _assignmentService.GetAnswer(id,studentId);
            ViewBag.assignment = item;
            return View(assignment);
        }

        public async Task<ActionResult> AssignmentDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = await _assignmentService.Get(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public async Task<ActionResult> AnswerDetail(int? id, int studentId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = await _assignmentService.GetAnswer(id, studentId);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }
        #endregion



        [AllowAnonymous]
        public JsonResult LgaList(string Id)
        {
            var stateId = db.States.FirstOrDefault(x => x.StateName == Id).Id;
            var local = from s in db.LocalGovs
                        where s.StatesId == stateId
                        select s;

            return Json(new SelectList(local.ToArray(), "LGAName", "LGAName"), JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> EditUser(int? id)
        {
            ViewBag.StateName = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _teacherProfileService.Get(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(TeacherProfile model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _teacherProfileService.Edit(model);
                    TempData["success"] = "Update Successful.";
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["error"] = "Update Unsuccessful, (" + e.ToString() + ")";
                }

            }
            return View(model);
        }





        public ActionResult Upload(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        // POST: Admin/Settings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upload(HttpPostedFileBase upload, int id)
        {
            try
            {
                var getteacher = await _teacherProfileService.Get(id);
                if (getteacher != null)
                {
                    if (getteacher.ImageId != 0)
                    {
                        await _imageService.Delete(getteacher.ImageId);
                    }
                    var imgId = await _imageService.Create(upload);

                    await _teacherProfileService.UpdateImageId(getteacher.Id, imgId);
                    TempData["success"] = "Update Successful.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["error"] = "Update Unsuccessful.";
            }
            return View();
        }
        #region

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Qualification()
        {
            var items = await _teacherProfileService.ListQualificationForUser();
            return View(items);
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


        #endregion


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