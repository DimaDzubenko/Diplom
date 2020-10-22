using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.Shop.Pentie
{
    public class PentieViewModel
    {
        public int PentieId { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public double Price { get; set; }
        public IFormFile Image { get; set; }

        public int? PBrandId { get; set; }
        public string PBrandName { get; set; }

        public int? PCategoryId { get; set; }
        public string PCategoryName { get; set; }

        public List<SelectListItem> PColors { get; set; }
        public string[] SelectedPColors { get; set; }
        public List<SelectListItem> PSizes { get; set; }
        public string[] SelectedPSizes { get; set; }
    }
}
