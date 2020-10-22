using Diplom.Services.Models.ShopCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.ShopCart
{
    public class CountryListViewModel
    {
        public IEnumerable<CountryDTO> Countries { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
