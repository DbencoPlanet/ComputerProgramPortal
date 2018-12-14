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
    public class PackageService : IPackageService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<Package> PackageDetails(int? id)
        {
            var item = db.Packages.Include(x => x.User).Where(x => x.Id == id);
            // var studnets = db.
            int c = await StudentsCount(id);
            //check user

            var output = item.Select(x => new Package
            {
                PackageName = x.PackageName,
                Id = x.Id,
                Fullname = x.User.Surname + " " + x.User.FirstName + " " + x.User.OtherName,
                NumberOfStudents = c,
                UserId = x.UserId
            });
            var outputmain = await output.FirstOrDefaultAsync();

            return outputmain;
        }

        public async Task<List<Package>> PackageList(string searchString, string currentFilter, int? page)
        {
            var item = db.Packages.Include(x => x.User).Include(x=>x.StudentInfos).Include(x=>x.TeacherProfiles).OrderBy(x => x.PackageName);

            //var output = item.Select(x => new Package
            //{
            //    PackageName = x.PackageName,
            //    Id = x.Id,
            //    UserId = x.UserId,
            //    Teacher = x.User.Surname + " " + x.User.FirstName + " " + x.User.OtherName
            //});
            return await item.ToListAsync();
        }

      

        public async Task Create(Package model)
        {
            db.Packages.Add(model);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var item = await db.Packages.FirstOrDefaultAsync(x => x.Id == id);
            db.Packages.Remove(item);
            await db.SaveChangesAsync();


        }

        public async Task Edit(Package models)
        {
            db.Entry(models).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<Package> Get(int? id)
        {
            var item = await db.Packages.Include(x => x.TeacherProfile).FirstOrDefaultAsync(x => x.Id == id);
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
            var enrolledStudentsId = db.Enrollments.Include(c => c.Package).Select(u => u.StudentInfoID);
            var allStudents = db.StudentInfos.Include(x => x.user).Where(x => enrolledStudentsId.Contains(x.Id)).Select(x => x.Id);
            var enrolledStudents = db.Enrollments.Include(e => e.StudentInfo).Include(x => x.Payment).Where(s => s.PackageID == id);
            return await enrolledStudents.CountAsync();
        }
    }
}