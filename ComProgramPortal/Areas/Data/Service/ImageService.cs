using ComProgramPortal.Areas.Data.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ComProgramPortal.Models.Entities;
using System.Threading.Tasks;
using ComProgramPortal.Models;
using System.Drawing;
using System.Web.Helpers;
using System.IO;
using System.Drawing.Imaging;

namespace ComProgramPortal.Areas.Data.Service
{
    public class ImageService : IImageService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //public HttpPostedFileBase ResizeBitmap(HttpPostedFileBase b, int nWidth, int nHeight)
        //{
        //    HttpPostedFileBase result = new HttpPostedFileBase(nWidth, nHeight);
        //    using (Graphics g = Graphics.FromImage((Image)result))
        //        g.DrawImage(b, 0, 0, nWidth, nHeight);
        //    return result;
        //}
        public async Task<int> Create(HttpPostedFileBase upload)
        {
            ImageModel model = new ImageModel();
            if (upload != null && upload.ContentLength > 0)
            {


                // Find its length and convert it to byte array
                int ContentLength = upload.ContentLength;

                // Create Byte Array
                byte[] bytImg = new byte[ContentLength];

                // Read Uploaded file in Byte Array
                upload.InputStream.Read(bytImg, 0, ContentLength);

                model.ImageContent = bytImg;
                model.ContentType = upload.ContentType;
                model.FileName = upload.FileName;

            }

            db.ImageModel.Add(model);
            await db.SaveChangesAsync();
            return model.Id;
        }

        public async Task Delete(int? id)
        {
            var img = await db.ImageModel.FirstOrDefaultAsync(x => x.Id == id);
            if (img != null)
            {
                db.ImageModel.Remove(img);
                await db.SaveChangesAsync();
            }


        }

        public async Task Edit(int id, HttpPostedFileBase upload)
        {
            var img = await db.ImageModel.FirstOrDefaultAsync(x => x.Id == id);
            if (img != null)
            {
                if (upload != null && upload.ContentLength > 0)
                {


                    // Find its length and convert it to byte array
                    int ContentLength = upload.ContentLength;

                    // Create Byte Array
                    byte[] bytImg = new byte[ContentLength];

                    // Read Uploaded file in Byte Array
                    upload.InputStream.Read(bytImg, 0, ContentLength);

                    img.ImageContent = bytImg;
                    img.ContentType = upload.ContentType;
                    img.FileName = upload.FileName;
                }
                db.Entry(img).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task<ImageModel> Get(int? id)
        {
            var img = await db.ImageModel.FirstOrDefaultAsync(x => x.Id == id);
            return img;
        }
    }
}