using ComProgramPortal.Models;
using ComProgramPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ComProgramPortal.Areas.Data.IService
{
    interface IUserManagerService
    {
        Task<string> NewTeacher(RegisterViewModel model);

        Task<TeacherProfile> GetTeacher(int? id);

        // Task Edit(NewStaffDto models);
        Task Delete(int? id);
        Task<ApplicationUser> GetUserByUserId(string id);
        Task<ApplicationUser> GetUserByUserEmail(string email);
        Task<string> NewStudent(RegisterViewModel model);
        Task<StudentInfo> GetStudent(int? id);

        Task<List<StudentInfo>> ListStudent(string searchString, string currentFilter, int? page);
        Task<List<TeacherProfile>> ListTeacher(string searchString, string currentFilter, int? page);
        Task<List<ApplicationUser>> AllUsers(string searchString, string currentFilter, int? page);
        Task<List<ApplicationUser>> Users();

        Task<bool> IsUsersinRole(string userid, string role);
        Task<bool> UpdateUser(ApplicationUser model);
        Task AddUserToRole(string userId, string rolename);
        Task RemoveUserToRole(string userId, string rolename);

        Task<List<ApplicationUser>> UserAll();
       
    }
}
