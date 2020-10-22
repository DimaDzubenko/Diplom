using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.DataModel.Shop.Penties
{
    public class PentieSize
    {
        public int Id { get; set; }
        // связь многие ко многим
        // cвязующая таблица для товара и размера
        public int PentieId { get; set; }
        public Pentie Pentie { get; set; }

        public int PSizeId { get; set; }
        public PSize PSize { get; set; }
    }
}
