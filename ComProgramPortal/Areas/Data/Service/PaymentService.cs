using ComProgramPortal.Areas.Data.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComProgramPortal.Models.Entities;
using System.Threading.Tasks;
using ComProgramPortal.Models;
using System.Data.Entity;


namespace ComProgramPortal.Areas.Data.Service
{
    public class PaymentService : IPaymentService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<Payment> PaymentDetails(int? id)
        {
            var item = db.Payments.Include(x => x.User).Where(x => x.Id == id);
            // var studnets = db.
            int c = await StudentsCount(id);
            //check user

            var output = item.Select(x => new Payment
            {
                //Package = x.Package,
                Id = x.Id,
                Fullname = x.User.Surname + " " + x.User.FirstName + " " + x.User.OtherName,
                //NumberOfStudents = c,
                UserId = x.UserId
            });
            var outputmain = await output.FirstOrDefaultAsync();

            return outputmain;

        }

        public async Task<List<Payment>> PaymentList(string searchString, string currentFilter, int? page)
        {
            var item = db.Payments.Include(x => x.User).OrderBy(x => x.PaymentDate);

            //var output = item.Select(x => new Package
            //{
            //    PackageName = x.PackageName,
            //    Id = x.Id,
            //    UserId = x.UserId,
            //    Teacher = x.User.Surname + " " + x.User.FirstName + " " + x.User.OtherName
            //});
            return await item.ToListAsync();
        }



        public async Task Create(Payment model)
        {
            db.Payments.Add(model);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var item = await db.Payments.FirstOrDefaultAsync(x => x.Id == id);
            db.Payments.Remove(item);
            await db.SaveChangesAsync();


        }

        public async Task Edit(Payment models)
        {
            db.Entry(models).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<Payment> Get(int? id)
        {
            var item = await db.Payments.Include(x => x.StudentInfos).FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }



        public async Task<List<StudentInfo>> Students(int? id)
        {


            var enrolledStudentsId = db.Enrollments.Include(c => c.Payment).Select(u => u.StudentInfoID);
            var allStudents = db.StudentInfos.Include(x => x.user).Where(x => enrolledStudentsId.Contains(x.Id)).Select(x => x.Id);
            var enrolledStudents = db.Enrollments.Include(e => e.StudentInfo).Include(x => x.Payment).Where(s => s.PackageID == id);

            var output = enrolledStudents.Select(x => new StudentInfo
            {
                Username = x.StudentInfo.user.UserName,
                Id = x.StudentInfoID,
                StudentRegNumber = x.StudentInfo.StudentRegNumber,
                Fullname = x.StudentInfo.user.Surname + " " + x.StudentInfo.user.FirstName + " " + x.StudentInfo.user.OtherName,
                Phone = x.StudentInfo.user.Phone,
                Email = x.StudentInfo.user.Email,
                PackageId = x.PackageID,
                EnrollmentId = x.StudentInfoID,
                UserId = x.StudentInfo.user.Id
            });
            return await output.ToListAsync();
        }

        public async Task<int> StudentsCount(int? id)
        {
            var enrolledStudentsId = db.Enrollments.Include(c => c.Payment).Select(u => u.StudentInfoID);
            var allStudents = db.StudentInfos.Include(x => x.user).Where(x => enrolledStudentsId.Contains(x.Id)).Select(x => x.Id);
            var enrolledStudents = db.Enrollments.Include(e => e.StudentInfo).Include(x => x.Payment).Where(s => s.PaymentID == id);
            return await enrolledStudents.CountAsync();
        }
    }
}