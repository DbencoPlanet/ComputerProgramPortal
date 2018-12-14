using ComProgramPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using ComProgramPortal.Models.Entities;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace ComProgramPortal.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DashBoardController : Controller
    {

        // GET: Admin/DashBoard
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            //Get Enrolled Students Count
            var enrolledStudents = db.Enrollments.Include(x => x.StudentInfo).Include(p => p.Package);

            ViewBag.EnrolledStudents = enrolledStudents.Count();

            //Get Staff Count
            var teacher = db.TeacherProfiles.Count();
            ViewBag.Teacher = teacher;

            ////

            var packages = db.Packages.Count();
            ViewBag.packages = packages;



            ///////
            //Get payment Count
            var fullpay = db.Payments.Where(x => x.Balance == 0).Count();
            ViewBag.Fullpay = fullpay;

            var hafpay = db.Payments.Where(x => x.Balance != 0).Count();
            ViewBag.Hafpay = hafpay;

            return View();
        }

       
    }
}