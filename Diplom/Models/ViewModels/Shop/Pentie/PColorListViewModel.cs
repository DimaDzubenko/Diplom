using Diplom.Services.Models.Shop.Penties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.Shop.Pentie
{
    public class PColorListViewModel
    {
        public IEnumerable<PColorDTO> PColors { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
