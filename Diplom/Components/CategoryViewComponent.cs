using Diplom.Services.Interfaces;
using Diplom.Services.Models.Shop.Penties;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        IService<PentieDTO> _service;

        public CategoryViewComponent(IService<PentieDTO> service)
        {
            _service = service;
        }

        public IViewComponentResult Invoke()
        {
            return View(_service.GetAll()
                .Select(p => p.PCategoryName)
                .Distinct()
                .OrderBy(p => p));
        }
    }
}
