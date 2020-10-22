using Diplom.Services.Models.Shop.Penties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.Shop.Pentie
{
    public class PentieListViewModel
    {
        public IEnumerable<PentieDTO> Penties { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public string CurrentCategory { get; set; }
        public string CurrentBrand { get; set; }
    }
}
