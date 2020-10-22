using Diplom.Services.Models.Blog;
using Diplom.Services.Models.Shop.Penties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels
{
    public class HomeListsViewModel
    {
        public IEnumerable<PBrandDTO> Brands { get; set; }
        public IEnumerable<PentieDTO> Penties { get; set; }
    }
}
