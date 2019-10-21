using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiFiles.BLL.Interfaces;

namespace WebApiFiles.BLL.Sevices
{
    public class ImageWorker : IImageWorker
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;
        

        public ImageWorker(IConfiguration configuration,
            IHostingEnvironment env)
        {
            _configuration = configuration;
            _env = env;

        }
        public string SaveImage(string base64)
        {
            string folderName = _configuration.GetValue<string>("ImagesPath");

            if (base64.Contains(","))
            {
                base64 = base64.Split(',')[1];
            }
            using (Bitmap bmp = this.ConvertToBitmap(base64))
            {
                if (bmp != null)
                {
                    string pathServerPhisics = _env.ContentRootPath;
                    string dirPathSave = Path.Combine(pathServerPhisics, folderName);
                    if(!Directory.Exists(dirPathSave))
                    {
                        Directory.CreateDirectory(dirPathSave);
                    }
                    var imageName = Path.GetRandomFileName() + ".jpg";

                    string fileSave = Path.Combine(dirPathSave, imageName);
                    bmp.Save(fileSave, ImageFormat.Jpeg);

                    return imageName;
                }
            }
            return null;
        }

        private Bitmap ConvertToBitmap(string base64)
        {
            try
            {
                byte[] byteBuffer = Convert.FromBase64String(base64);
                using (MemoryStream memoryStream = new MemoryStream(byteBuffer))
                {
                    memoryStream.Position = 0;
                    using (Image imgReturn = Image.FromStream(memoryStream))
                    {
                        memoryStream.Close();
                        byteBuffer = null;
                        return new Bitmap(imgReturn);
                    }
                }
            }
            catch(Exception ex) { return null; }
        }
    }
}
