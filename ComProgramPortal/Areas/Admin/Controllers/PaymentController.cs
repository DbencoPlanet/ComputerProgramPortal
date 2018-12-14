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

namespace ComProgramPortal.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class PaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Payment
        public async Task<ActionResult> Index()
        {
            return View(await db.Payments.Include(x => x.User).OrderByDescending(x=>x.PaymentDate).ToListAsync());
        }

        //GET: Admin/Payment/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = await db.Payments.FindAsync(id);
            //var item = db.Payments.Include(x => x.User).Where(x => x.Id == id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Admin/Payment/Create
        public ActionResult Create(int? id)
        {
            //ViewBag.StudentName = new SelectList(db.StudentInfos, "Id", "Surname");
            ViewBag.StudentName = new SelectList(db.StudentInfos.Include(x => x.user), "UserId", "Firstname");
            ViewBag.PackageName = new SelectList(db.Packages, "Id", "PackageName");
            return View();
        }

        // POST: Admin/Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,UserId,PackageId,Fullname,PaymentDate,PaymentAmount,Balance")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                TempData["success"] = "Payment has been Added Successfully";
                await db.SaveChangesAsync();
                TempData["failure"] = "Package was not created";
                return RedirectToAction("Index");
            }

            ViewBag.StudentName = new SelectList(db.StudentInfos.Include(x=>x.user), "UserId", "Firstname", payment.UserId);
            ViewBag.PackageName = new SelectList(db.Packages, "Id", "PackageName",payment.PackageId);
            return View(payment);
        }

        // GET: Admin/Payment/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = await db.Payments.FindAsync(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentName = new SelectList(db.StudentInfos.Include(x=>x.user), "UserId", "Firstname", payment.UserId);
            ViewBag.PackageName = new SelectList(db.Packages, "Id", "PackageName", payment.PackageId);
            return View(payment);
        }

        // POST: Admin/Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UserId,PackageId,Fullname,PackageName,PaymentDate,PaymentAmount,Balance")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                TempData["success"] = "Payment has been Updated Successfully";
                await db.SaveChangesAsync();
                TempData["failure"] = "Payment was not updated try again";
                return RedirectToAction("Index");
            }
            ViewBag.StudentName = new SelectList(db.StudentInfos.Include(x=>x.user), "UserId", "Firstname", payment.UserId);
            ViewBag.PackageName = new SelectList(db.Packages, "Id", "PackageName", payment.PackageId);
            return View(payment);
        }

        // GET: Admin/Payment/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = await db.Payments.FindAsync(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Admin/Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Payment payment = await db.Payments.FindAsync(id);
            db.Payments.Remove(payment);
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
