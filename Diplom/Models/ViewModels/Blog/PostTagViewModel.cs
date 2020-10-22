using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.Blog
{
    public class PostTagViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter the post")]
        public int PostId { get; set; }
        public string PostName { get; set; }
        [Required(ErrorMessage = "Enter the tag of post")]
        public int TagId { get; set; }
        public string TagName { get; set; }
    }
}
