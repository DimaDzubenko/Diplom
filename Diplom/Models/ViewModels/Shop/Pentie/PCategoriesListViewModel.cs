﻿using Diplom.Services.Models.Shop.Penties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ViewModels.Shop.Pentie
{
    public class PCategoriesListViewModel
    {
        public IEnumerable<PCategoryDTO> PCategories { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
