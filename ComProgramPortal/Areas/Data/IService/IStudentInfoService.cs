using ComProgramPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComProgramPortal.Areas.Data.IService
{
    interface IStudentInfoService
    {
        Task<StudentInfo> Get(int? id);
        Task UpdateImageId(int id, int imgId);
        Task Edit(StudentInfo models);

        Task<List<StudentInfo>> List();

        Task<StudentInfo> GetStudentWithoutId();
        Task<StudentInfo> GetStudentWithId(int? profileId);
        Task<StudentInfo> GetStudentByUserId(string id);
        Task<string> PackageNameForStudent();
        Task<string> PackageNameForStudent2(string id);
        //Task<string> PackageNameForStudent2();
    }
}
