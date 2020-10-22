using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.Shop.Pentie
{
    public class PBrandViewModel
    {
        public int PBrandId { get; set; }
        [Required(ErrorMessage = "Brand name is required")]
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
