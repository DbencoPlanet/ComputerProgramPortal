using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComProgramPortal.Models.Entities
{
    public class StudentInfo
    {
       
            public int Id { get; set; }

            public string UserId { get; set; }

            public ApplicationUser user { get; set; }

            public string Fullname { get; set; }
            public int? PackageId { get; set; }
            public int? PaymentId { get; set; }
            public int? EnrollmentId { get; set; }
            


        //public string Fullname
        //{
        //    get
        //    {
        //        return Surname + " " + Firstname;
        //    }
        //}
        public string Surname { get; set; }
            public string Firstname { get; set; }
            public string Othername { get; set; }
           public string Email { get; set; }

            public string Username { get; set; }

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

            [Display(Name = "Local Government Area")]
            public string LocalGov { get; set; }

            [Display(Name = "State Of Origin")]
            public string StateOfOrigin { get; set; }

            public string AboutMe { get; set; }

            public string FavouriteFood { get; set; }

            public string BooksYouLike { get; set; }

           public string MoviesYouLike { get; set; }

           public string TVShowsYouLike { get; set; }

           public string YourLikes { get; set; }

           public string YourDisLikes { get; set; }

           public string FavouriteColour { get; set; }


           public string PreviousCompKnowledge { get; set; }

           public string ParentGuardianName { get; set; }

           public string ParentGuardianAddress { get; set; }

           public string ParentGuardianPhoneNumber { get; set; }

           public string ParentGuardianOccupation { get; set; }

           public string Nationality { get; set; }

           public string Disability { get; set; }

           public string EmergencyContact { get; set; }
           public string StudentRegNumber { get; set; }

        //public string StudentRegNumber
        //{
        //    get
        //    {
        //        return "STD" + "/ " + Id;
        //    }
        //}
        public string RegisteredBy { get; set; }
        public byte[] Image { get; set; }
        public int ImageId { get; set; }

        //public virtual ICollection<File> Files { get; set; }

        //public int EnrollmentID { get; set; }
        public virtual ICollection<Enrollment> Enrollment { get; set; }

        //public int PackageID { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}