using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.Shop.Pentie
{
    public class PCategoryViewModel
    {
        public int? PCategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; }
    }
}
