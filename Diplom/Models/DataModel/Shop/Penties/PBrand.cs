using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.DataModel.Shop.Penties
{
    public class PBrand
    {
        public int PBrandId { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }

        // связь один ко многим
        // реализация что у производителя может быть много товаров
        public List<Pentie> Penties { get; set; }
    }
}
