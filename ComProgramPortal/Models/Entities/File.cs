using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComProgramPortal.Models.Entities
{
    public class File
    {
        public int FileId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType FileType { get; set; }
        //public int StudentInfoId { get; set; }
        //public virtual StudentInfo StudentInfo { get; set; }

        //public int PackageId { get; set; }
        //public virtual Package Package { get; set; }

        //public int TeacherProfileId { get; set; }
        //public virtual TeacherProfile TeacherProfile { get; set; }
    }
}