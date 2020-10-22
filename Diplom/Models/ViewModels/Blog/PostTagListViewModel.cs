using Diplom.Services.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.Blog
{
    public class PostTagListViewModel
    {
        public IEnumerable<PostTagDTO> PostTags { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
