using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.DataModel.Shop.Penties
{
    public class PColor
    {
        public int PColorId { get; set; }
        public string Name { get; set; }

        // связь многие ко многим
        public List<PentieColor> PentiColors { get; set; }
    }
}
