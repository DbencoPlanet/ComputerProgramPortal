using ComProgramPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComProgramPortal.Areas.Data.IService
{
    interface IEnrollmentService
    {
        Task Create(Enrollment model);
        Task<Enrollment> Get(int? id);
        Task<StudentInfo> GetStudent(int? id);

        Task Edit(Enrollment models);
        Task Delete(int? id);
        Task<List<Enrollment>> List();
    }
}
