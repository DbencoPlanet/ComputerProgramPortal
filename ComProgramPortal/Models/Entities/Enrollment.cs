using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComProgramPortal.Models.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int? PackageID { get; set; }
        public int StudentInfoID { get; set; }
        public int? PaymentID { get; set; }


        public virtual Package Package { get; set; }
        public virtual StudentInfo StudentInfo { get; set; }
        public virtual Payment Payment { get; set; }
        //public virtual ICollection<Payment> Payment { get; set; }

    }

}