using Diplom.Services.Models.Shop.Penties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.Shop.Pentie
{
    public class PentieSizeListViewModel
    {
        public IEnumerable<PentieSizeDTO> PentieSizes { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public string CurrentPentie { get; set; }
        public string CurrentColor { get; set; }
    }
}
