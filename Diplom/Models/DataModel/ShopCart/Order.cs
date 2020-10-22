using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.DataModel.ShopCart
{
    public class Order
    {
        public Order()
        {
            CreatedOn = DateTimeOffset.Now;
        }

        public int OrderId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CustomerId { get; set; }
        public int? AddressId { get; set; }
        public int? CountryId { get; set; }
        public int TotalValue { get; set; }

        public virtual Address Address { get; set; }
    }
}
