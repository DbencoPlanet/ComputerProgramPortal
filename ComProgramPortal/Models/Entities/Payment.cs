using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComProgramPortal.Models.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Fullname { get; set; }

        public int? PackageId { get; set; }
        public virtual Package Package { get; set; }


        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

       
        [Display(Name = "Amount Paid")]
        public decimal PaymentAmount { get; set; }

       
        [Display(Name = "Balance Remaining")]
        public decimal Balance { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        //public virtual ICollection<Package> Packages { get; set; }
        public virtual ICollection<StudentInfo> StudentInfos { get; set; }
       
       

    }
}