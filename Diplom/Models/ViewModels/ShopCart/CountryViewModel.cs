using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.ShopCart
{
    public class CountryViewModel
    {
        public int? CountryId { get; set; }

        [Required(ErrorMessage = "Country name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Country short name is required")]
        public string ShortName { get; set; }
    }
}
