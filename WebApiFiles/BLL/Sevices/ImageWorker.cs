using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFiles.BLL.Interfaces;

namespace WebApiFiles.BLL.Sevices
{
    public class ImageWorker : IImageWorker
    {
        private readonly IConfiguration _configuration;

        public ImageWorker(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public string SaveImage(string base64)
        {
            string folderName = _configuration.GetValue<string>("ImagesPath");

            throw new NotImplementedException();
        }
    }
}
