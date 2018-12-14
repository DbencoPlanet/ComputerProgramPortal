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

namespace ComProgramPortal.Areas.Student.Controllers
{
    [Authorize(Roles = "Student,Admin,SuperAdmin")]
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


        // GET: Student/Panel
        public async Task<ActionResult> Index()
        {
            try
            {
                var profile = await _studentInfoService.GetStudentWithoutId();
                ViewBag.student = profile;

                //var sub = await _studentInfoService.SubjectCountStudent();
                //ViewBag.SubjectCount = sub;

                //var post = await _postService.ListPost(7);
                //ViewBag.post = post;

                var package = await _studentInfoService.PackageNameForStudent();
                ViewBag.Package = package;

            }
            catch (Exception ex)
            {
                var a = ex;
            }
            return View();
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
                var getstudent = await _studentInfoService.Get(id);
                if (getstudent != null)
                {
                    if (getstudent.ImageId != 0)
                    {
                        await _imageService.Delete(getstudent.ImageId);
                    }
                    var imgId = await _imageService.Create(upload);

                    await _studentInfoService.UpdateImageId(getstudent.Id, imgId);
                    TempData["success"] = "Update Successful.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Update Unsuccessful." + ex;
            }
            return View();
        }

        public async Task<ActionResult> EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _studentInfoService.Get(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName", user.StateOfOrigin);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(StudentInfo model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _studentInfoService.Edit(model);
                    TempData["success"] = "Update Successful.";
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["error"] = "Update Unsuccessful, (" + e.ToString() + ")";
                }

            }
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName", model.StateOfOrigin);
            return View(model);
        }

        #region

        public async Task<ActionResult> Assignment()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var studentId = await db.StudentInfos.Include(x => x.user).FirstOrDefaultAsync(x => x.UserId == userid);
                var enroment = db.Enrollments.Include(x => x.Package).Include(x => x.User).Where(x => x.StudentInfoID == studentId.Id).Select(x => x.PackageID).ToList();

                var myass = db.Assignments.Include(x => x.Package).Include(x => x.AssignmentAnswers).Where(x => enroment.Contains(x.PackageId) && x.IsPublished == true);

                return View(myass);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

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
            var user = User.Identity.GetUserId();
            var check = await db.AssignmentAnswers.FirstOrDefaultAsync(x => x.AssignmentId == id && x.UserId == user);
            if (check != null)
            {
                if (item.DateSubmitionEnds > DateTime.UtcNow.Date)
                {
                    ViewBag.status = "<span class=\"\" style=\"font-weight:bolder;\">You Have Submited and can EDIT</span>";
                }
                else
                {
                    ViewBag.status = "<span class=\"text-success\" style=\"font-weight:bolder;\">You Have Submited and CANNOT EDIT </span>";
                }
            }
            else
            {
                if (item.DateSubmitionEnds > DateTime.UtcNow.Date)
                {
                    ViewBag.status = "<span class=\"text-warning\" style=\"font-weight:bolder;\">You Have Not Submited</span>";
                }
                else
                {
                    ViewBag.status = "<span class=\"text-danger\" style=\"font-weight:bolder;\">You Missed the Assignment</span>";
                }
            }
            return View(item);
        }

        public async Task<ActionResult> AnswerDetail(int? id,int studentId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = await _assignmentService.GetAnswer(id,studentId);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public async Task<ActionResult> AssignmentAnswer(int? assId)
        {
            var user = User.Identity.GetUserId();
            //var stuntid = db.StudentInfos.Include(x => x.user).Where(x => x.Id == student);
            var check = await db.AssignmentAnswers.FirstOrDefaultAsync(x => x.AssignmentId == assId && x.UserId == user);
            if (check != null)
            {
                return RedirectToAction("EditAssignmentAnswer", new { id = check.AssignmentId });
            }
            var item = await _assignmentService.Get(assId);
            ViewBag.assignment = item;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignmentAnswer(AssignmentAnswer model, int? assId)
        {
            var check = await db.AssignmentAnswers.FirstOrDefaultAsync(x => x.AssignmentId == assId);
            if (check != null)
            {
                return RedirectToAction("EditAssignmentAnswer", new { id = check.AssignmentId });
            }
            if (ModelState.IsValid)
            {

                var userid = User.Identity.GetUserId();
                var studentId = await db.StudentInfos.FirstOrDefaultAsync(x => x.UserId == userid);
                var enrollement = await db.Enrollments.Include(x => x.Package).Include(x => x.StudentInfo).FirstOrDefaultAsync(x => x.StudentInfoID == studentId.Id);
                model.AssignmentId = assId;
                model.PackageId = enrollement.PackageID;
                model.EnrollementId = enrollement.Id;
                model.StudentId = studentId.Id;
                model.UserId = userid;
               

                model.DateAnswered = DateTime.UtcNow.AddHours(1);
                await _assignmentService.CreateAnswer(model);
                TempData["success"] = "Submitted Successful";
                return RedirectToAction("Assignment");
            }
            TempData["error"] = "Unable to Save";
            return View(model);
        }

        public async Task<ActionResult> EditAssignmentAnswer(int id)
        {
            var userid = User.Identity.GetUserId();
            var studentId = await db.StudentInfos.FirstOrDefaultAsync(x => x.UserId == userid);
            var item = await _assignmentService.GetAnswer(id, studentId.Id);
            var assinfo = await _assignmentService.Get(item.AssignmentId);
            ViewBag.assignment = assinfo;
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAssignmentAnswer(AssignmentAnswer model, int id)
        {
            if (ModelState.IsValid)
            {

                model.DateModified = DateTime.UtcNow.AddHours(1);
                await _assignmentService.EditAnswer(model);
                TempData["success"] = "Submitted Successful";
                return RedirectToAction("Assignment");
            }
            TempData["error"] = "Unable to Save";
            return View(model);
        }

        #endregion

        #region
        [AllowAnonymous]
        ////public async Task<ActionResult> Profile (string id)
        public async Task<ActionResult> StudentProfile(int? id)
        {
            //var userid = User.Identity.GetUserId();
            ////var myid = await db.StudentInfos.Include(x => x.user).FirstOrDefaultAsync(x => x.UserId == id);
            //var student = await _studentInfoService.GetStudentByUserId(userid);
            //var currentpackage = await _studentInfoService.PackageNameForStudent(id);
            //ViewBag.currentpackage = currentpackage;
            //return RedirectToAction("StudentProfile");
            try
            {
                var profile = await _studentInfoService.GetStudentWithoutId();
                ViewBag.student = profile;

                //var sub = await _studentInfoService.SubjectCountStudent();
                //ViewBag.SubjectCount = sub;

                //var post = await _postService.ListPost(7);
                //ViewBag.post = post;

                var package = await _studentInfoService.PackageNameForStudent();
                ViewBag.Package = package;

            }
            catch (Exception ex)
            {
                var a = ex;
            }
            return View();

        }

        //[AllowAnonymous]
        //public async Task<ActionResult> StudentProfile(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var student = await _studentInfoService.GetStudentWithId(id);
        //    var currentpackage = await _studentInfoService.PackageNameForStudent(id);
        //    ViewBag.currentpackage = currentpackage;
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}
        #endregion

        public JsonResult LgaList(string Id)
        {
            var stateId = db.States.FirstOrDefault(x => x.StateName == Id).Id;
            var LGA = from s in db.LocalGovs
                      where s.StatesId == stateId
                      select s;

            return Json(new SelectList(LGA.ToArray(), "LGAName", "LGAName"), JsonRequestBehavior.AllowGet);
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