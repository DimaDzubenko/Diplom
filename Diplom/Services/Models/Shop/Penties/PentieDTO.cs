using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Services.Models.Shop.Penties
{
    public class PentieDTO
    {
        public int PentieId { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }

        public int? PBrandId { get; set; }
        public string PBrandName { get; set; }
        public int? PCategoryId { get; set; }
        public string PCategoryName { get; set; }
    }
}
