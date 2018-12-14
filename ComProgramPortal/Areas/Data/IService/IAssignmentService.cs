using ComProgramPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComProgramPortal.Areas.Data.IService
{
    interface IAssignmentService
    {
        Task Create(Assignment model);
        Task Edit(Assignment model);
        Task<Assignment> Get(int? id);
       

        Task Delete(int? id);
        Task<List<Assignment>> List(int? packageId);
        Task<List<AssignmentAnswer>> ListForStudent(int packageId, int assignmentId);
        ///

        ////
        ///answers for assignment
        Task CreateAnswer(AssignmentAnswer model);
        Task EditAnswer(AssignmentAnswer model);
        Task<AssignmentAnswer> GetAnswer(int? id, int? studentId);

        Task DeleteAnswer(int? id);

    }
}
