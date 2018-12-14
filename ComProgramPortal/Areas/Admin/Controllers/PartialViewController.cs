using ComProgramPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComProgramPortal.Areas.Admin.Controllers
{
    public class PartialViewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/PartialView
        public ActionResult ProfileImage()
        {

            var item = db.ImageModel.FirstOrDefault();
            return PartialView(item);


        }
    }
}
