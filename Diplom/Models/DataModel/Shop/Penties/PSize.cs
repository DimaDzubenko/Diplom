using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.DataModel.Shop.Penties
{
    public class PSize
    {
        public int PSizeId { get; set; }
        public string Name { get; set; }

        // связь многие ко многим
        public List<PentieSize> PentieSizes { get; set; }
    }
}
