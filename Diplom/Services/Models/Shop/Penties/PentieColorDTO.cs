using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Services.Models.Shop.Penties
{
    public class PentieColorDTO
    {
        public int Id { get; set; }
        public int PentieId { get; set; }
        public string PentieName { get; set; }

        public int ColorId { get; set; }
        public string ColorName { get; set; }
    }
}
