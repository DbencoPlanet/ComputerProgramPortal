using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComProgramPortal.Models.Entities
{
    public class TeacherQualification
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Name of Institution")]
        public string NameOfInstitution { get; set; }

        [Required]
        [Display(Name = "Year Obtained")]
        public string YearObtained { get; set; }

        [Required]
        [Display(Name = "Certificate Obtained")]
        public string CertificateObtained { get; set; }


        public int TeacherProfileID { get; set; }
        //public string UserId { get; set; }
        public TeacherProfile TeacherProfile { get; set; }
    }
}