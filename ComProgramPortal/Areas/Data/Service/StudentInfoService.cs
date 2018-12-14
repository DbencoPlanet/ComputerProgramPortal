using ComProgramPortal.Areas.Data.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComProgramPortal.Models.Entities;
using System.Threading.Tasks;
using ComProgramPortal.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ComProgramPortal.Areas.Data.Service
{
    public class StudentInfoService : IStudentInfoService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public StudentInfoService()
        {
        }

        public StudentInfoService(ApplicationUserManager userManager
           )
        {
            UserManager = userManager;

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

        public async Task Edit(StudentInfo model)
        {
            StudentInfo student = await db.StudentInfos.FindAsync(model.Id);
            student.Disability = model.Disability;
            student.EmergencyContact = model.EmergencyContact;
            student.AboutMe = model.AboutMe;
            student.FavouriteFood = model.FavouriteFood;
            student.BooksYouLike = model.BooksYouLike;
            student.MoviesYouLike = model.MoviesYouLike;
            student.TVShowsYouLike = model.TVShowsYouLike;
            student.YourLikes = model.YourLikes;
            student.YourDisLikes = model.YourDisLikes;
            student.FavouriteColour = model.FavouriteColour;
            student.ContactAddress = model.ContactAddress;
            student.PreviousCompKnowledge = model.PreviousCompKnowledge;
            student.ParentGuardianAddress = model.ParentGuardianAddress;
            student.ParentGuardianName = model.ParentGuardianName;
            student.ParentGuardianPhoneNumber = model.ParentGuardianPhoneNumber;
            student.ParentGuardianOccupation = model.ParentGuardianOccupation;


            db.Entry(student).State = EntityState.Modified;
            await db.SaveChangesAsync();

            ApplicationUser user = await UserManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                if (HttpContext.Current.User.IsInRole("Admin, SuperAdmin"))
                {
                    user.Email = model.Email;
                    user.FirstName = model.Firstname;
                    user.Surname = model.Surname;
                    user.OtherName = model.Othername;
                }
                user.DateOfBirth = model.DateOfBirth;
                user.Religion = model.Religion;
                user.Gender = model.Gender;
                user.ContactAddress = model.ContactAddress;
                user.City = model.City;
                user.Phone = model.Phone;
                user.LocalGov = model.LocalGov;
                user.StateOfOrigin = model.StateOfOrigin;
                user.Nationality = model.Nationality;
                // UserManager.Update(user);
                await UserManager.UpdateAsync(user);
            }

        }

        public async Task UpdateImageId(int id, int imgId)
        {
            StudentInfo model = await db.StudentInfos.FindAsync(id);
            model.ImageId = imgId;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<StudentInfo> Get(int? id)
        {
            var student = await db.StudentInfos.Include(x => x.user).FirstOrDefaultAsync(x => x.Id == id);

            byte[] c;
            var img = await db.ImageModel.FirstOrDefaultAsync(x => x.Id == student.ImageId);
            if (img == null)
            {
                c = new byte[0];
            }
            else
            {
                c = img.ImageContent;
            }

            var output = new StudentInfo
            {
                Id = student.Id,
                Disability = student.Disability,
                StudentRegNumber = student.StudentRegNumber,
                PreviousCompKnowledge = student.PreviousCompKnowledge,
                ParentGuardianAddress = student.ParentGuardianAddress,
                ParentGuardianName = student.ParentGuardianName,
                ParentGuardianOccupation = student.ParentGuardianOccupation,
                ParentGuardianPhoneNumber = student.ParentGuardianPhoneNumber,
                AboutMe = student.AboutMe,
                FavouriteFood = student.FavouriteFood,
                BooksYouLike = student.BooksYouLike,
                MoviesYouLike = student.MoviesYouLike,
                TVShowsYouLike = student.TVShowsYouLike,
                YourLikes = student.YourLikes,
                YourDisLikes = student.YourDisLikes,
                FavouriteColour = student.FavouriteColour,
                Fullname = student.user.Surname + " " + student.user.FirstName + " " + student.user.OtherName,
                Surname = student.user.Surname,
                Firstname = student.user.FirstName,
                Othername = student.user.OtherName,
                Email = student.user.Email,
                Username = student.user.UserName,
                DateOfBirth = student.user.DateOfBirth,
                DateRegistered = student.user.DateRegistered,
                DataStatusChanged = student.user.DataStatusChanged,
                Religion = student.user.Religion,
                Gender = student.user.Gender,
                ContactAddress = student.user.ContactAddress,
                City = student.user.City,
                Phone = student.user.Phone,
                LocalGov = student.user.LocalGov,
                StateOfOrigin = student.user.StateOfOrigin,
                Nationality = student.user.Nationality,
                RegisteredBy = student.user.RegisteredBy,
                ImageId = student.ImageId,
                Image = c,
                UserId = student.user.Id

            };
            return output;
        }


        public async Task<StudentInfo> GetStudentWithoutId()
        {
            var user = HttpContext.Current.User.Identity.GetUserId();
            var student = await db.StudentInfos.Include(x => x.user).FirstOrDefaultAsync(x => x.UserId == user);
            //var student = await db.StudentInfos.Include(x => x.Enrollment).Include(x => x.Packages).Include(x => x.Payments).Include(x => x.user).FirstOrDefaultAsync(x => x.UserId == user);
            byte[] c;
            var img = await db.ImageModel.FirstOrDefaultAsync(x => x.Id == student.ImageId);
            if (img == null)
            {
                c = new byte[0];
            }
            else
            {
                c = img.ImageContent;
            }

            var output = new StudentInfo
            {
                Id = student.Id,
                Disability = student.Disability,
                StudentRegNumber = student.StudentRegNumber,
                PreviousCompKnowledge = student.PreviousCompKnowledge,
                ParentGuardianAddress = student.ParentGuardianAddress,
                ParentGuardianName = student.ParentGuardianName,
                ParentGuardianOccupation = student.ParentGuardianOccupation,
                ParentGuardianPhoneNumber = student.ParentGuardianPhoneNumber,
                AboutMe = student.AboutMe,
                FavouriteFood = student.FavouriteFood,
                BooksYouLike = student.BooksYouLike,
                MoviesYouLike = student.MoviesYouLike,
                TVShowsYouLike = student.TVShowsYouLike,
                YourLikes = student.YourLikes,
                YourDisLikes = student.YourDisLikes,
                FavouriteColour = student.FavouriteColour,
                Fullname = student.user.Surname + " " + student.user.FirstName + " " + student.user.OtherName,
                Surname = student.user.Surname,
                Firstname = student.user.FirstName,
                Othername = student.user.OtherName,
                Email = student.user.Email,
                Username = student.user.UserName,
                DateOfBirth = student.user.DateOfBirth,
                DateRegistered = student.user.DateRegistered,
                DataStatusChanged = student.user.DataStatusChanged,
                Religion = student.user.Religion,
                Gender = student.user.Gender,
                ContactAddress = student.user.ContactAddress,
                City = student.user.City,
                Phone = student.user.Phone,
                LocalGov = student.user.LocalGov,
                StateOfOrigin = student.user.StateOfOrigin,
                Nationality = student.user.Nationality,
                RegisteredBy = student.user.RegisteredBy,
                ImageId = student.ImageId,
                Image = c,
                UserId = student.user.Id

            };
            return output;
        }

        public async Task<List<StudentInfo>> List()
        {
            var items = await db.StudentInfos.Include(x => x.user).ToListAsync();
            return items;
        }

        public async Task<string> PackageNameForStudent()
        {
            string item = "Empty";
            var user = HttpContext.Current.User.Identity.GetUserId();
            var pId = await db.StudentInfos.Include(x => x.user).FirstOrDefaultAsync(x => x.UserId == user);
            var enrol = await db.Enrollments.Include(x => x.Package).FirstOrDefaultAsync(x=>x.StudentInfoID == pId.Id);
            if (enrol != null)
            {
                item = enrol.Package.PackageName;
            }

            return item;
        }


        public async Task<string> PackageNameForStudent2(string id)
        {
            string item = "Empty";
            var user = HttpContext.Current.User.Identity.GetUserId();
            var pId = await db.StudentInfos.Include(x => x.user).FirstOrDefaultAsync(x => x.UserId == user);
            var enrol = await db.Enrollments.Include(x => x.Package).FirstOrDefaultAsync(x => x.StudentInfoID == pId.Id);
            if (enrol != null)
            {
                item = enrol.Package.PackageName;
            }

            return item;
        }


        public async Task<StudentInfo> GetStudentWithId(int? profileId)
        {
            var user = HttpContext.Current.User.Identity.GetUserId();
            var student = await db.StudentInfos.Include(x => x.user).FirstOrDefaultAsync(x => x.UserId == user);
            //var student = await db.StudentInfos.Include(x => x.Enrollment).Include(x => x.Packages).Include(x => x.Payments).Include(x => x.user).FirstOrDefaultAsync(x => x.UserId == user);
            var enrol = await db.Enrollments.Include(x => x.Package).FirstOrDefaultAsync(x => x.StudentInfoID == profileId);
            byte[] c;
            var img = await db.ImageModel.FirstOrDefaultAsync(x => x.Id == student.ImageId);
            if (img == null)
            {
                c = new byte[0];
            }
            else
            {
                c = img.ImageContent;
            }

            var output = new StudentInfo
            {
                Id = student.Id,
                Disability = student.Disability,
                StudentRegNumber = student.StudentRegNumber,
                PreviousCompKnowledge = student.PreviousCompKnowledge,
                ParentGuardianAddress = student.ParentGuardianAddress,
                ParentGuardianName = student.ParentGuardianName,
                ParentGuardianOccupation = student.ParentGuardianOccupation,
                ParentGuardianPhoneNumber = student.ParentGuardianPhoneNumber,
                AboutMe = student.AboutMe,
                FavouriteFood = student.FavouriteFood,
                BooksYouLike = student.BooksYouLike,
                MoviesYouLike = student.MoviesYouLike,
                TVShowsYouLike = student.TVShowsYouLike,
                YourLikes = student.YourLikes,
                YourDisLikes = student.YourDisLikes,
                FavouriteColour = student.FavouriteColour,
                Fullname = student.user.Surname + " " + student.user.FirstName + " " + student.user.OtherName,
                Surname = student.user.Surname,
                Firstname = student.user.FirstName,
                Othername = student.user.OtherName,
                Email = student.user.Email,
                Username = student.user.UserName,
                DateOfBirth = student.user.DateOfBirth,
                DateRegistered = student.user.DateRegistered,
                DataStatusChanged = student.user.DataStatusChanged,
                Religion = student.user.Religion,
                Gender = student.user.Gender,
                ContactAddress = student.user.ContactAddress,
                City = student.user.City,
                Phone = student.user.Phone,
                LocalGov = student.user.LocalGov,
                StateOfOrigin = student.user.StateOfOrigin,
                Nationality = student.user.Nationality,
                RegisteredBy = student.user.RegisteredBy,
                ImageId = student.ImageId,
                Image = c,
                UserId = student.user.Id

            };
            return output;
        }

        public async Task<StudentInfo> GetStudentByUserId(string id)
        {
            var student = await db.StudentInfos.FirstOrDefaultAsync(x => x.UserId == id);
            return student;
        }
    }
}