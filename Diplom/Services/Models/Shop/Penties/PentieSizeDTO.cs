using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Services.Models.Shop.Penties
{
    public class PentieSizeDTO
    {
        public int Id { get; set; }
        public int PentieId { get; set; }
        public string PentieName { get; set; }

        public int SizeId { get; set; }
        public string SizeName { get; set; }
    }
}
