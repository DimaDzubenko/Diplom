using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.Shop.Pentie
{
    public class PSizeViewModel
    {
        public int? PSizeId { get; set; }

        [Required(ErrorMessage = "Size name is required")]
        public string Name { get; set; }
    }
}
