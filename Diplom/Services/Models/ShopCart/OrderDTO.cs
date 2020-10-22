using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Services.Models.ShopCart
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int? AddressId { get; set; }
        public int? CountryId { get; set; }
        public string AddressLine { get; set; }
        public int TotalValue { get; set; }
    }
}
