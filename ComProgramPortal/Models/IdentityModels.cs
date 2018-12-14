using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ComProgramPortal.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System;

namespace ComProgramPortal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string Surname { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Other Name")]
        public string OtherName { get; set; }
        public string Fullname { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateRegistered { get; set; }
        public DateTime? DataStatusChanged { get; set; }

        public string Religion { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Contact Address")]
        public string ContactAddress { get; set; }
        public string City { get; set; }

        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Display(Name = "Phone Number")]
        public string PackageName { get; set; }

        [Display(Name = "Local Government Area")]
        public string LocalGov { get; set; }

        [Display(Name = "State Of Origin")]
        public string StateOfOrigin { get; set; }

        public string Nationality { get; set; }
        public string RegisteredBy { get; set; }
        public bool? IsLocked { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Enrollment> Enrollments { get; set; }
        //public DbSet<File> Files { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<StudentInfo> StudentInfos { get; set; }
        public DbSet<StudentInfoData> StudentInfoDatas { get; set; }
        public DbSet<TeacherProfile> TeacherProfiles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentAnswer> AssignmentAnswers { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<LocalGovs> LocalGovs { get; set; }
        public DbSet<TeacherQualification> TeacherQualifications { get; set; }
        public DbSet<ImageModel> ImageModel { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}