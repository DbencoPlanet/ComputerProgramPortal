using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ComProgramPortal.Models;
using ComProgramPortal.Models.Entities;
using PagedList;

namespace ComProgramPortal.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class EnrollmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Enrollment
        //public async Task<ActionResult> Index(string currentFilter,string searchString, int? page)
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            var enrollments = db.Enrollments.Include(e => e.Package).Include(e => e.Payment).Include(e => e.StudentInfo);

            if (searchString != null)
            {
               
                  page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            //var students = enrollments.Select(x => new Enrollment
            //{

            //    Username = x.StudentInfo.user.UserName,
            //    Id = x.StudentInfo.Id,
            //    Surname = x.StudentInfo.user.Surname,
            //    Firstname = x.StudentInfo.user.FirstName,
            //    Othername = x.StudentInfo.user.OtherName,
            //    EnrollmentId = x.StudentInfoID,
            //    StudentRegNumber = x.StudentInfo.StudentRegNumber,
            //    PackageId = x.PackageID
            //});

            //var students = from s in db.StudentInfos
            
            if (!String.IsNullOrEmpty(searchString))
            {
                enrollments = enrollments.Where(s =>
               s.StudentInfo.Surname.ToUpper().Contains(searchString.ToUpper())
                ||
               s.StudentInfo.Firstname.ToUpper().Contains(searchString.ToUpper())
                 ||
               s.StudentInfo.Othername.ToUpper().Contains(searchString.ToUpper()));
            }
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(enrollments.OrderBy(x => x.StudentInfo.Surname).ToPagedList(pageNumber, pageSize));
        }


        // GET: Admin/Enrollment
        public async Task<ActionResult> EnrolledStudent()
        {
            var enrollments = db.Enrollments.Include(e => e.Package).Include(e => e.Payment).Include(e => e.StudentInfo);
            var count = await db.Enrollments.CountAsync();
            ViewBag.Total = count;
            
            return View(await enrollments.ToListAsync());
        }

        // GET: Admin/Enrollment/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Admin/Enrollment/Create
        public ActionResult Create()
        {

            ViewBag.PackageID = new SelectList(db.Packages, "Id", "PackageName");
            ViewBag.PaymentID = new SelectList(db.Payments, "Id", "PaymentAmount");
            ViewBag.StudentInfoID = new SelectList(db.StudentInfos, "Id", "Fullname");
            return View();
        }

        // POST: Admin/Enrollment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,PackageID,StudentInfoID,PaymentID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PackageID = new SelectList(db.Packages, "Id", "PackageName", enrollment.PackageID);
            ViewBag.PaymentID = new SelectList(db.Payments, "Id", "PaymentAmount", enrollment.PaymentID);
            ViewBag.StudentInfoID = new SelectList(db.StudentInfos, "Id", "Fullname", enrollment.StudentInfoID);
            return View(enrollment);
        }

        // GET: Admin/Enrollment/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PackageID = new SelectList(db.Packages, "Id", "PackageName", enrollment.PackageID);
            ViewBag.PaymentID = new SelectList(db.Payments, "Id", "PaymentAmount", enrollment.PaymentID);
            ViewBag.StudentInfoID = new SelectList(db.StudentInfos, "Id", "Fullname", enrollment.StudentInfoID); 
            return View(enrollment);
        }

        // POST: Admin/Enrollment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PackageID,PaymentID,StudentInfoID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PackageID = new SelectList(db.Packages, "Id", "PackageName", enrollment.PackageID);
            ViewBag.PaymentID = new SelectList(db.Payments, "Id", "PaymentAmount", enrollment.PaymentID);
            ViewBag.StudentInfoID = new SelectList(db.StudentInfos, "Id", "Fullname", enrollment.StudentInfoID);
            return View(enrollment);
        }

        // GET: Admin/Enrollment/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Admin/Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            db.Enrollments.Remove(enrollment);
            await db.SaveChangesAsync();
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
