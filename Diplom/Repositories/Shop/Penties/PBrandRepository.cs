﻿using Diplom.Interfaces;
using Diplom.Models.DataModel.Shop.Penties;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Repositories.Shop.Penties
{
    public class PBrandRepository : GenericRepository<PBrand>
    {
        public PBrandRepository(IConfiguration configuration) : base(configuration) { }
    }
}
