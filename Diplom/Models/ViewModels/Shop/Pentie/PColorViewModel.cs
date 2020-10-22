using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.Shop.Pentie
{
    public class PColorViewModel
    {
        public int? PColorId { get; set; }

        [Required(ErrorMessage = "Color name is required")]
        public string Name { get; set; }
    }
}
