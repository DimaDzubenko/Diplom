using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.DataModel.ShopCart
{
    public class Address
    {
        public int AddressId { get; set; }
        public string AddressLine { get; set; }
        public string ContactName { get; set; }
        public int? CountryId { get; set; }
        public string City { get; set; }

        public virtual Country Country { get; set; }
    }
}
