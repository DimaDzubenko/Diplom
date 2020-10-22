using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.Blog
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public IFormFile Image { get; set; }

        public List<SelectListItem> Tags { get; set; }
        public string[] SelectedTags { get; set; }
    }
}
