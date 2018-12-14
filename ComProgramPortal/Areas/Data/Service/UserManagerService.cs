using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using ComProgramPortal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ComProgramPortal.Models.Entities;
using ComProgramPortal.Areas.Data.IService;

namespace ComProgramPortal.Areas.Data.Service
{

    public class UserManagerService : IUserManagerService
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        public UserManagerService()
        {
        }

        public UserManagerService(ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }


        public Task Delete(int? id)
        {
            throw new NotImplementedException();
        }

        //public Task Edit(NewStaffDto models)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<TeacherProfile> GetTeacher(int? id)
        {
            var teacher = await db.TeacherProfiles.Include(x => x.user).FirstOrDefaultAsync(x => x.Id == id);
            if (teacher != null)
            {
                return teacher;
            }
            return null;
        }

        public async Task<StudentInfo> GetStudent(int? id)
        {
            var student = await db.StudentInfos.Include(x => x.user).FirstOrDefaultAsync(x => x.Id == id);
            if (student != null)
            {
                return student;
            }
            return null;
        }

        public async Task<ApplicationUser> GetUserByUserId(string id)
        {
            var student = await UserManager.FindByIdAsync(id);
            if (student != null)
            {
                return student;
            }
            return null;
        }



        public async Task<string> NewTeacher(RegisterViewModel model)
        {
            //var setting = db.Settings.OrderByDescending(x => x.Id).First();
            var user1 = HttpContext.Current.User.Identity.GetUserName();
            if (user1 == "SuperAdmin")
            {
                user1 = "Admin";
            }
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Surname = model.Surname,
                Email = model.Email,
                FirstName = model.FirstName,
                OtherName = model.OtherName,
                DateOfBirth = model.DateOfBirth,
                Religion = model.Religion,
                PasswordHash = model.Password,
                //DateRegistered = DateTime.UtcNow,
                DateRegistered = DateTime.UtcNow.AddHours(1),
                Phone = model.Phone,
                ContactAddress = model.ContactAddress,
                City = model.City,
                StateOfOrigin = model.StateOfOrigin,
                Nationality = model.Nationality,
                RegisteredBy = user1
            };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
               
               TeacherProfile teacher = new TeacherProfile();
                teacher.UserId = user.Id;
                teacher.Username = user.UserName;
                teacher.Surname = user.Surname;
                teacher.Firstname = user.FirstName;
                teacher.Fullname = user.Surname + " " + user.FirstName  + " " + user.OtherName;
                teacher.Email = user.Email;
                teacher.Phone = user.Phone;
                teacher.DateOfAppointment = DateTime.UtcNow.AddHours(1);
                 db.TeacherProfiles.Add(teacher);
                 await UserManager.AddToRoleAsync(user.Id, "Teacher");
                await db.SaveChangesAsync();

                var teacherReg = await db.TeacherProfiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
                teacherReg.StaffRegistrationId = "TEACHER/000" + teacherReg.Id;
                db.Entry(teacherReg).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return "true";
            }
            var errors = result.Errors;
            var message = string.Join(", ", errors);

            return message;
        }



        public async Task<string> NewStudent(RegisterViewModel model)
        {

            var user1 = HttpContext.Current.User.Identity.GetUserName();
            if (user1 == "SuperAdmin")
            {
                user1 = "Admin";
            }
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                Surname = model.Surname,
                FirstName = model.FirstName,
                OtherName = model.OtherName,
                Fullname = model.Surname + " " + model.FirstName + " " + model.OtherName,
                DateOfBirth = model.DateOfBirth,
                Religion = model.Religion,
                Phone = model.Phone,
                DateRegistered = DateTime.UtcNow.AddHours(1),
                ContactAddress = model.ContactAddress,
                City = model.City,
                StateOfOrigin = model.StateOfOrigin,
                Nationality = model.Nationality,
                RegisteredBy = user1
            };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(user.Id, "Student");

                StudentInfo student = new StudentInfo();
                student.UserId = user.Id;
                student.Username = user.Surname;
                student.Firstname = user.FirstName;
                student.Othername = user.OtherName;
                student.Fullname = user.Surname + " " + user.FirstName + " " + user.OtherName;
                student.StateOfOrigin = user.StateOfOrigin;
                student.LocalGov = user.LocalGov;
                student.Email = user.Email;
                student.Phone = user.Phone;
                student.Religion = user.Religion;

                db.StudentInfos.Add(student);
                await db.SaveChangesAsync();

                var studentReg = await db.StudentInfos.FirstOrDefaultAsync(x => x.UserId == user.Id);
                studentReg.StudentRegNumber = "STUDENT/000" + studentReg.Id;
                db.Entry(studentReg).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return "true";
            }

            string error = string.Join(" ", result.Errors);
            return error;
        }

        public async Task<List<StudentInfo>> ListStudent(string searchString, string currentFilter, int? page)
        {
            var list = db.StudentInfos.Include(x => x.user);
            if (!String.IsNullOrEmpty(searchString))
            {
                if (CountString(searchString) > 1)
                {
                    string[] searchStringCollection = searchString.Split(' ');

                    foreach (var item in searchStringCollection)
                    {
                        list = list.Where(s => s.user.Surname.ToUpper().Contains(item.ToUpper()) || s.user.FirstName.ToUpper().Contains(item.ToUpper())
                                                               || s.user.OtherName.ToUpper().Contains(item.ToUpper()) || s.StudentRegNumber.ToUpper().Contains(item.ToUpper()) || s.user.UserName.ToUpper().Contains(item.ToUpper()));
                    }
                }
                else
                {
                    list = list.Where(s => s.user.Surname.ToUpper().Contains(searchString.ToUpper()) || s.user.FirstName.ToUpper().Contains(searchString.ToUpper())
                                                               || s.user.OtherName.ToUpper().Contains(searchString.ToUpper()) || s.StudentRegNumber.ToUpper().Contains(searchString.ToUpper()) || s.user.UserName.ToUpper().Contains(searchString.ToUpper()));
                }

            }
            return await list.ToListAsync();
        }

        public async Task<List<TeacherProfile>> ListTeacher(string searchString, string currentFilter, int? page)
        {
           
            var list = db.TeacherProfiles.Include(x => x.user);
            if (!String.IsNullOrEmpty(searchString))
            {
                if (CountString(searchString) > 1)
                {
                    string[] searchStringCollection = searchString.Split(' ');

                    foreach (var item in searchStringCollection)
                    {
                        list = list.Where(s => s.user.Surname.ToUpper().Contains(item.ToUpper()) || s.user.FirstName.ToUpper().Contains(item.ToUpper())
                                                               || s.user.OtherName.ToUpper().Contains(item.ToUpper()) || s.StaffRegistrationId.ToUpper().Contains(item.ToUpper()) || s.user.UserName.ToUpper().Contains(item.ToUpper()));
                    }
                }
                else
                {
                    list = list.Where(s => s.user.Surname.ToUpper().Contains(searchString.ToUpper()) || s.user.FirstName.ToUpper().Contains(searchString.ToUpper())
                                                               || s.user.OtherName.ToUpper().Contains(searchString.ToUpper()) || s.StaffRegistrationId.ToUpper().Contains(searchString.ToUpper()) || s.user.UserName.ToUpper().Contains(searchString.ToUpper()));
                }

            }
            return await list.ToListAsync();
        }

        public async Task<List<ApplicationUser>> UserAll()
        {

            var users = UserManager.Users.Where(x => x.UserName != "SuperAdmin").OrderBy(x => x.UserName);
            return await users.ToListAsync();
        }


        public async Task<List<ApplicationUser>> AllUsers(string searchString, string currentFilter, int? page)
        {

            var users = UserManager.Users.Where(x => x.UserName != "SuperAdmin");
            if (!String.IsNullOrEmpty(searchString))
            {
                if (CountString(searchString) > 1)
                {
                    string[] searchStringCollection = searchString.Split(' ');

                    foreach (var item in searchStringCollection)
                    {
                        users = users.Where(s => s.Surname.ToUpper().Contains(item.ToUpper()) || s.FirstName.ToUpper().Contains(item.ToUpper())
                                                               || s.OtherName.ToUpper().Contains(item.ToUpper()) || s.UserName.ToUpper().Contains(item.ToUpper()));
                    }
                }
                else
                {
                    users = users.Where(s => s.Surname.ToUpper().Contains(searchString.ToUpper()) || s.FirstName.ToUpper().Contains(searchString.ToUpper())
                                                               || s.OtherName.ToUpper().Contains(searchString.ToUpper()) || s.UserName.ToUpper().Contains(searchString.ToUpper()));
                }

            }

            return await users.OrderBy(x => x.Surname).ToListAsync();
        }

      
        public async Task<bool> IsUsersinRole(string userid, string role)
        {
            var users = await _userManager.IsInRoleAsync(userid, role);
            return users;
        }

      
        ///
        public int CountString(string searchString)
        {
            int result = 0;

            searchString = searchString.Trim();

            if (searchString == "")
                return 0;

            while (searchString.Contains("  "))
                searchString = searchString.Replace("  ", " ");

            foreach (string y in searchString.Split(' '))

                result++;


            return result;
        }


        public async Task<List<ApplicationUser>> Users()
        {
            return (await UserManager.Users.ToListAsync());
        }

        public async Task AddUserToRole(string userId, string rolename)
        {
            await UserManager.AddToRoleAsync(userId, rolename);
        }
        public async Task RemoveUserToRole(string userId, string rolename)
        {
            await UserManager.RemoveFromRoleAsync(userId, rolename);
        }

        public async Task<bool> UpdateUser(ApplicationUser model)
        {


            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;

        }

        public async Task<ApplicationUser> GetUserByUserEmail(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}