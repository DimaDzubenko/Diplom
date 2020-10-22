using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Diplom.Models;
using Diplom.Services.Interfaces;
using Diplom.Services.Models.Blog;
using Diplom.Services.Models.Shop.Penties;
using Diplom.Models.ViewModels;

namespace Diplom.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IService<PBrandDTO> _brandService;
        IService<PentieDTO> _pentieService;

        public HomeController(ILogger<HomeController> logger, IService<PBrandDTO> brandService, IService<PentieDTO> pentieService)
        {
            _logger = logger;
            _brandService = brandService;
            _pentieService = pentieService;
        }

        public IActionResult Index()
        {
            var brands = _brandService.GetAll();
            var penties = _pentieService.GetAll();
            return View(new HomeListsViewModel
            {
                Penties = penties,
                Brands = brands
            });
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
