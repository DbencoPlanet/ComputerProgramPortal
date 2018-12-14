using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComProgramPortal.Models.Entities
{
    public class AssignmentAnswer
    {
        public int Id { get; set; }
        public int? AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public DateTime DateAnswered { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int? StudentId { get; set; }
        public StudentInfo StudentInfo { get; set; }
        public int? PackageId { get; set; }
        public Package Package { get; set; }
        public int? EnrollementId { get; set; }
        public Enrollment Enrollement { get; set; }
        [AllowHtml]
        public string Answer { get; set; }
        public bool Assessed { get; set; }
    }
}