using ComProgramPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComProgramPortal.Areas.Data.IService
{
    interface ITeacherProfileService
    {
        Task<TeacherProfile> Get(int? id);
        Task UpdateImageId(int id, int imgId);
        Task<TeacherProfile> GetTeacherWithoutId();
        Task Edit(TeacherProfile models);

        Task<List<TeacherProfile>> List();
        ////

        Task<int> CreateQualification(TeacherQualification model);
        Task<TeacherQualification> GetQualification(int? id);
        Task<int> EditQualification(TeacherQualification models);
        Task<int> DeleteQualification(int? id);
        Task<List<TeacherQualification>> ListQualification(int id);
        Task<List<TeacherQualification>> ListQualificationForUser();


        Task<List<Package>> PackagesByTeacher();
        //Task<StudentInfo> StudentsList(int packageId);

        Task<List<Package>> TeacherPackageName();

        Task<List<TeacherProfile>> TeacherDropdownList();





    }
}
