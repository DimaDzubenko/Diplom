using Diplom.Interfaces;
using Diplom.Models.DataModel.ShopCart;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Repositories.ShopCart
{
    public class AddressRepository : GenericRepository<Address>
    {
        public AddressRepository(IConfiguration configuration) : base(configuration) { }
    }
}
