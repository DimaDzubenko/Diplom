using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.DataModel.Shop.Penties
{
    public class PentieColor
    {
        public int Id { get; set; }
        // связь многие ко многим
        // cвязующая таблица для товара и цвета
        public int PentieId { get; set; }
        public Pentie Pentie { get; set; }

        public int PColorId { get; set; }
        public PColor PColor { get; set; }
    }
}
