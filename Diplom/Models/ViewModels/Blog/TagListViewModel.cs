using Diplom.Services.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.Blog
{
    public class TagListViewModel
    {
        public IEnumerable<TagDTO> Tags { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
