using Diplom.Models.BasketModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Components
{
    public class BasketTotalViewComponent : ViewComponent
    {
        private readonly Basket basket;

        public BasketTotalViewComponent(Basket _basket)
        {
            basket = _basket;
        }

        public IViewComponentResult Invoke() => View(basket);
    }
}
