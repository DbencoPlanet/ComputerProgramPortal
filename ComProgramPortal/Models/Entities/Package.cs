using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComProgramPortal.Models.Entities
{
    public class Package
    {
        [Display(Name = "Package ID")]
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Fullname { get; set; }

        public int NumberOfStudents { get; set; }

        //public int PaymentId { get; set; }
        //public Payment Payment { get; set; }

        //public int? TeacherId { get; set; }
        public TeacherProfile TeacherProfile { get; set; }

        [Display(Name = "Package Name")]
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Name of the package is required")]
        public string PackageName { get; set; }

       
        [Display(Name = "Package Duration")]
        public string Duration { get; set; }

        
        [Display(Name = "Package Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Package End Date")]
        public DateTime EndDate { get; set; }

       
        [Display(Name = "Package Price Tag")]
        public decimal PackageAmount { get; set; }

        [AllowHtml]
        public string PackageInfo { get; set; }

        public byte[] Image { get; set; }
        public int ImageId { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        //public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<StudentInfo> StudentInfos { get; set; }
        public virtual ICollection<TeacherProfile> TeacherProfiles { get; set; }
        public virtual ICollection<File> Files { get; set; }
        //public virtual ICollection<Assignment> Assingments { get; set; }

    }
}