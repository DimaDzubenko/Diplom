using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Services.Models.Blog
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public byte[] Image { get; set; }

    }
}
