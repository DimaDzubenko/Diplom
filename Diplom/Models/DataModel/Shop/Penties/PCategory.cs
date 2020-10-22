using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.DataModel.Shop.Penties
{
    public class PCategory
    {
        public int PCategoryId { get; set; }
        public string Name { get; set; }

        // связь один ко многим
        // реализация что у категории может быть много товаров
        public List<Pentie> Penties { get; set; }
    }
}
