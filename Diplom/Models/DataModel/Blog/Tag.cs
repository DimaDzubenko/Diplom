using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.DataModel.Blog
{
    public class Tag
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
