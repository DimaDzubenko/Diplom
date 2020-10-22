using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.DataModel.Shop.Penties
{
    public class Pentie
    {
        public int PentieId { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }

        // связь один ко многим
        // реализация что у товара может быть один производитель
        public int? PBrandId { get; set; }
        public PBrand PBrand { get; set; }
        // реализация что у товара может быть одина категория
        public int? PCategoryId { get; set; }
        public PCategory PCategory { get; set; }

        // связь многие ко многим
        public List<PentieColor> PentiColors { get; set; }
        public List<PentieSize> PentieSizes { get; set; }
    }
}
