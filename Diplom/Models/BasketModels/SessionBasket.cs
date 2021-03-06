﻿using Diplom.Extensions;
using Diplom.Models.ViewModels.Shop.Pentie;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.BasketModels
{
    public class SessionBasket : Basket
    {
        public static Basket GetBasket(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            SessionBasket basket = session?.GetJson<SessionBasket>("Basket") ??
                new SessionBasket();
            basket.Session = session;
            return basket;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(PentieViewModel pentie, int quantity)
        {
            base.AddItem(pentie, quantity);
            Session.SetJson("Basket", this);
        }

        public override void RemoveItem(PentieViewModel product)
        {
            base.RemoveItem(product);
            Session.SetJson("Basket", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Basket");
        }
    }
}
