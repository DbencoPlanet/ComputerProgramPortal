using ComProgramPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ComProgramPortal.Areas.Data.IService
{
    interface IImageService
    {
        Task<int> Create(HttpPostedFileBase upload);
        Task<ImageModel> Get(int? id);
        Task Edit(int id, HttpPostedFileBase upload);
        Task Delete(int? id);
    }
}
