using ComProgramPortal.Areas.Data.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComProgramPortal.Models.Entities;
using System.Threading.Tasks;
using ComProgramPortal.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;

namespace ComProgramPortal.Areas.Data.Service
{
    public class TeacherProfileService : ITeacherProfileService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public TeacherProfileService()
        {
        }

        public TeacherProfileService(ApplicationUserManager userManager
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

        public async Task UpdateImageId(int id, int imgId)
        {
            TeacherProfile model = await db.TeacherProfiles.FindAsync(id);
            model.ImageId = imgId;
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
        public async Task<List<Package>> PackagesByTeacher()
        {
            var item = db.Packages.Where(x => x.PackageName != "");
            if (HttpContext.Current.User.IsInRole("Admin") || HttpContext.Current.User.IsInRole("SuperAdmin"))
            {
                item = item.OrderBy(x => x.PackageName);

            }
            else
            {
                var currentUser = HttpContext.Current.User.Identity.GetUserId();
                item = item.Where(x => x.UserId == currentUser);

            }
            return await item.OrderBy(x => x.PackageName).ToListAsync();
        }

        public async Task<int> CreateQualification(TeacherQualification model)
        {
            var userid = HttpContext.Current.User.Identity.GetUserId();
            var teacherid = await db.TeacherProfiles.FirstOrDefaultAsync(x => x.UserId == userid);
            model.UserId = userid;
            model.TeacherProfileID = teacherid.Id;
            db.TeacherQualifications.Add(model);
            await db.SaveChangesAsync();
            return model.Id;
        }

        public async Task<int> DeleteQualification(int? id)
        {
            var item = await db.TeacherQualifications.FirstOrDefaultAsync(x => x.Id == id);
            var idi = item.Id;
            if (item != null)
            {

                db.TeacherQualifications.Remove(item);

                await db.SaveChangesAsync();
            }
            return idi;
        }

        public async Task Edit(TeacherProfile model)
        {
            TeacherProfile teacher = await db.TeacherProfiles.FindAsync(model.Id);
            teacher.Disability = model.Disability;
            teacher.EmergencyContact = model.EmergencyContact;
            teacher.AboutMe = model.AboutMe;
            teacher.FavouriteFood = model.FavouriteFood;
            teacher.BooksYouLike = model.BooksYouLike;
            teacher.MoviesYouLike = model.MoviesYouLike;
            teacher.TVShowsYouLike = model.TVShowsYouLike;
            teacher.YourLikes = model.YourLikes;
            teacher.YourDisLikes = model.YourDisLikes;
            teacher.FavouriteColour = model.FavouriteColour;
            teacher.MaritalStatus = model.MaritalStatus;
            db.Entry(teacher).State = EntityState.Modified;
            await db.SaveChangesAsync();

            //   ApplicationUser user = _userManager.FindById(model.userid);
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
                await UserManager.UpdateAsync(user);
            }
        }

        public async Task<int> EditQualification(TeacherQualification models)
        {
            db.Entry(models).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return models.Id;
        }

        public async Task<TeacherProfile> Get(int? id)
        {
            var teacher = await db.TeacherProfiles.Include(x => x.user).FirstOrDefaultAsync(x => x.Id == id);
            byte[] c;
            var img = await db.ImageModel.FirstOrDefaultAsync(x => x.Id == teacher.ImageId);
            if (img == null)
            {
                c = new byte[0];
            }
            else
            {
                c = img.ImageContent;
            }
            var output = new TeacherProfile
            {
                Id = teacher.Id,
                Disability = teacher.Disability,
                EmergencyContact = teacher.EmergencyContact,
                MaritalStatus = teacher.MaritalStatus,
                StaffRegistrationId = teacher.StaffRegistrationId,
                DateOfAppointment = teacher.DateOfAppointment,
                AboutMe = teacher.AboutMe,
                FavouriteFood = teacher.FavouriteFood,
                BooksYouLike = teacher.BooksYouLike,
                MoviesYouLike = teacher.MoviesYouLike,
                TVShowsYouLike = teacher.TVShowsYouLike,
                YourLikes = teacher.YourLikes,
                YourDisLikes = teacher.YourDisLikes,
                FavouriteColour = teacher.FavouriteColour,
                Fullname = teacher.user.Surname + " " + teacher.user.FirstName + " " + teacher.user.OtherName,
                Surname = teacher.user.Surname,
                Firstname = teacher.user.FirstName,
                Othername = teacher.user.OtherName,
                Email = teacher.user.Email,
                Username = teacher.user.UserName,
                DateOfBirth = teacher.user.DateOfBirth,
                DateRegistered = teacher.user.DateRegistered,
                DataStatusChanged = teacher.user.DataStatusChanged,
                Religion = teacher.user.Religion,
                Gender = teacher.user.Gender,
                ContactAddress = teacher.user.ContactAddress,
                City = teacher.user.City,
                Phone = teacher.user.Phone,
                LocalGov = teacher.user.LocalGov,
                StateOfOrigin = teacher.user.StateOfOrigin,
                Nationality = teacher.user.Nationality,
                RegisteredBy = teacher.user.RegisteredBy,
                ImageId = teacher.ImageId,
                Image = c,
                UserId = teacher.user.Id

            };
            return output;

        }

        public async Task<TeacherQualification> GetQualification(int? id)
        {
            var item = await db.TeacherQualifications.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<TeacherProfile> GetTeacherWithoutId()
        {
            var user = HttpContext.Current.User.Identity.GetUserId();
            var teacher = await db.TeacherProfiles.Include(x => x.user).FirstOrDefaultAsync(x => x.UserId == user);
            byte[] c;
            var img = await db.ImageModel.FirstOrDefaultAsync(x => x.Id == teacher.ImageId);
            if (img == null)
            {
                c = new byte[0];
            }
            else
            {
                c = img.ImageContent;
            }
            var output = new TeacherProfile
            {
                Id = teacher.Id,
                Disability = teacher.Disability,
                EmergencyContact = teacher.EmergencyContact,
                MaritalStatus = teacher.MaritalStatus,
                StaffRegistrationId = teacher.StaffRegistrationId,
                DateOfAppointment = teacher.DateOfAppointment,
                AboutMe = teacher.AboutMe,
                FavouriteFood = teacher.FavouriteFood,
                BooksYouLike = teacher.BooksYouLike,
                MoviesYouLike = teacher.MoviesYouLike,
                TVShowsYouLike = teacher.TVShowsYouLike,
                YourLikes = teacher.YourLikes,
                YourDisLikes = teacher.YourDisLikes,
                FavouriteColour = teacher.FavouriteColour,
                Fullname = teacher.user.Surname + " " + teacher.user.FirstName + " " + teacher.user.OtherName,
                Surname = teacher.user.Surname,
                Firstname = teacher.user.FirstName,
                Othername = teacher.user.OtherName,
                Email = teacher.user.Email,
                Username = teacher.user.UserName,
                DateOfBirth = teacher.user.DateOfBirth,
                DateRegistered = teacher.user.DateRegistered,
                DataStatusChanged = teacher.user.DataStatusChanged,
                Religion = teacher.user.Religion,
                Gender = teacher.user.Gender,
                ContactAddress = teacher.user.ContactAddress,
                City = teacher.user.City,
                Phone = teacher.user.Phone,
                LocalGov = teacher.user.LocalGov,
                StateOfOrigin = teacher.user.StateOfOrigin,
                Nationality = teacher.user.Nationality,
                RegisteredBy = teacher.user.RegisteredBy,
                ImageId = teacher.ImageId,
                Image = c,
                UserId = teacher.user.Id

            };
            return output;

        }

        public async Task<List<TeacherProfile>> List()
        {
            var items = await db.TeacherProfiles.Include(x => x.TeacherQualifications).Include(x => x.user).ToListAsync();
            return items;
        }

        public async Task<List<TeacherQualification>> ListQualification(int id)
        {
            var items = await db.TeacherQualifications.Include(x => x.TeacherProfile).ToListAsync();
            return items;
        }

        public async Task<List<TeacherQualification>> ListQualificationForUser()
        {
            var user = HttpContext.Current.User.Identity.GetUserId();
            var userProfileId = await db.TeacherProfiles.Include(x => x.user).FirstOrDefaultAsync(f => f.UserId == user);
            var items = await db.TeacherQualifications.Include(x => x.TeacherProfile).Where(x => x.TeacherProfileID == userProfileId.Id).ToListAsync();
            return items;
        }

        public async Task<List<TeacherProfile>> TeacherDropdownList()
        {

            var item = db.TeacherProfiles.Include(x => x.user);
            var output = item.Select(x => new TeacherProfile
            {
                Id = x.Id,
                Fullname = x.user.Surname + " " + x.user.FirstName + " " + x.user.OtherName,
                UserId = x.UserId
            });
            return await output.ToListAsync();

        }

        public async Task<List<Package>> TeacherPackageName()
        {
            var user = HttpContext.Current.User.Identity.GetUserId();
            var name = await db.Packages.Where(x => x.UserId == user).ToListAsync();

            return name;
        }


        //public Task<TeacherProfile> StudentsList(int packageId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}