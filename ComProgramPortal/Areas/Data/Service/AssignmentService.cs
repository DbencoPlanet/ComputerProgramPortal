using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ComProgramPortal.Models;
using ComProgramPortal.Models.Entities;
using System.Data;
using System.Threading.Tasks;
using ComProgramPortal.Areas.Data.IService;

namespace ComProgramPortal.Areas.Data.Service
{
    public class AssignmentService : IAssignmentService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task Create(Assignment model)
        {
            db.Assignments.Add(model);
            await db.SaveChangesAsync();
        }

        public async Task CreateAnswer(AssignmentAnswer model)
        {
            db.AssignmentAnswers.Add(model);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var item = await db.Assignments.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                db.Assignments.Remove(item);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAnswer(int? id)
        {
            var item = await db.AssignmentAnswers.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                db.AssignmentAnswers.Remove(item);
                await db.SaveChangesAsync();
            }
        }

        public async Task Edit(Assignment model)
        {
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task EditAnswer(AssignmentAnswer model)
        {
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<Assignment> Get(int? id)
        {
            var item = await db.Assignments.Include(x=>x.User).Include(x => x.Package).Include(x => x.AssignmentAnswers).FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

       

        public async Task<AssignmentAnswer> GetAnswer(int? id, int? studentId)
        {
            var item = await db.AssignmentAnswers.Include(x => x.Assignment).Include(x=>x.User).Include(x=>x.Package).Include(x => x.StudentInfo).Include(x => x.Enrollement).FirstOrDefaultAsync(x => x.AssignmentId == id && x.StudentId == studentId);
            return item;
        }

        public async Task<List<Assignment>> List(int? packageId)
        {
            var assign = db.Assignments.Include(x => x.AssignmentAnswers).Include(x => x.Package).Where(x => x.PackageId == packageId && x.IsPublished == true);
            return await assign.ToListAsync();
        }

        public async Task<List<AssignmentAnswer>> ListForStudent(int packageId, int assignmentId)
        {
            var assign1 = db.AssignmentAnswers.Include(x => x.Assignment).Include(x => x.User).Include(x => x.StudentInfo).Include(x => x.Package);
            var assign = assign1.Where(x => x.PackageId == packageId && x.AssignmentId == assignmentId);
            return await assign.ToListAsync();
        }
    }
}