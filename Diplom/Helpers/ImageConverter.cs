using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Helpers
{
    public class ImageConverter
    {
        public static byte[] GetBytes(IFormFile file)
        {
            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                return binaryReader.ReadBytes((int)file.Length);
            }
        }
    }
}
