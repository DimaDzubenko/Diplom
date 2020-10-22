using Diplom.Models.BasketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.ShopCart
{
    public class BasketViewModel
    {
        public Basket Basket { get; set; }
        public string ReturnUrl { get; set; }
    }
}
