using ComProgramPortal.Areas.Data.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComProgramPortal.Models.Entities;
using System.Threading.Tasks;
using ComProgramPortal.Models;
using System.Data;
using System.Data.Entity;

namespace ComProgramPortal.Areas.Data.Service
{
    public class EnrollmentService : IEnrollmentService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task Create(Enrollment model)
        {
            db.Enrollments.Add(model);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var item = await db.Enrollments.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                db.Enrollments.Remove(item);
                await db.SaveChangesAsync();
            }
        }

        public async Task Edit(Enrollment models)
        {
            db.Entry(models).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }


        public async Task<Enrollment> Get(int? id)
        {
            var item = await db.Enrollments.Include(x => x.Package).Include(x => x.Payment).Include(x => x.User).Include(x => x.StudentInfo).FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<List<Enrollment>> List()
        {
            var items = await db.Enrollments.ToListAsync();
            return items;
        }


        ///
     
    
      
        public async Task<StudentInfo> GetStudent(int? id)
        {
            
            Enrollment enrollment = db.Enrollments.Find(id);

            var user = await db.StudentInfos.Include(x => x.user).FirstOrDefaultAsync(x => x.Id == enrollment.StudentInfoID);

            return user;
        }



        public string Fullname(int id)
        {
            var user = db.StudentInfos.Include(x => x.user).FirstOrDefault(x => x.Id == id);
            string name = user.user.Surname + " " + user.user.FirstName + " " + user.user.OtherName;
            return name;
        }

        ///
    }
}