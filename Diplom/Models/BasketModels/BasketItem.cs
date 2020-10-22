using Diplom.Models.ViewModels.Shop.Pentie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.BasketModels
{
    public class BasketItem
    {
        public int CartItemId { get; set; }
        public PentieViewModel Pentie { get; set; }
        public int Quantity { get; set; }
    }
}
