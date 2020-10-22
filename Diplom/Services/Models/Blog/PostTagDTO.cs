using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Services.Models.Blog
{
    public class PostTagDTO
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string PostName { get; set; }

        public int TagId { get; set; }
        public string TagName { get; set; }
    }
}
