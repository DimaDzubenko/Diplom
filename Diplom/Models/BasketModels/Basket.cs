using Diplom.Models.ViewModels.Shop.Pentie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.BasketModels
{
    public class Basket
    {
        private List<BasketItem> itemCollection = new List<BasketItem>();

        public virtual void AddItem(PentieViewModel pentie, int quantity)
        {
            BasketItem item = itemCollection
                .Where(p => p.Pentie.PentieId == pentie.PentieId).FirstOrDefault();

            if (item == null)
            {
                itemCollection.Add(new BasketItem
                {
                    Pentie = pentie,
                    Quantity = quantity
                });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public virtual void RemoveItem(PentieViewModel pentie)
        {
            itemCollection.RemoveAll(i => i.Pentie.PentieId == pentie.PentieId);
        }

        public virtual double ComputeTotalValue() => itemCollection.Sum(e => e.Pentie.Price * e.Quantity);

        public virtual void Clear() => itemCollection.Clear();

        public virtual IEnumerable<BasketItem> Items => itemCollection;
    }
}
