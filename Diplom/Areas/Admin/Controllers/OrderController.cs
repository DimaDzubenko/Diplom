using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diplom.Data;
using Diplom.Models.BasketModels;
using Diplom.Models.DataModel.Identity;
using Diplom.Models.ViewModels;
using Diplom.Models.ViewModels.ShopCart;
using Diplom.Services.Interfaces;
using Diplom.Services.Models.ShopCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Diplom.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        private readonly IService<OrderDTO> _orderService;
        private readonly IService<AddressDTO> _addressService;
        private readonly IService<CountryDTO> _countriesService;
        private readonly Basket _basket;
        private readonly IConfiguration _configuration;
        //private readonly IEmailSender emailSender;

        public int PageSize { get; set; } = 10;

        public OrderController(IService<OrderDTO> orderService,
                               IService<AddressDTO> addressService,
                               IService<CountryDTO> countriesService,
                               Basket basket,
                               ApplicationDbContext context,
                               UserManager<User> userManager,
                               SignInManager<User> signInManager,
                               IConfiguration configuration)
        //IEmailSender _emailSender)
        {
            _orderService = orderService;
            _addressService = addressService;
            _countriesService = countriesService;
            _basket = basket;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            //emailSender = _emailSender;
        }

        public ActionResult Index(int page = 1)
        {
            var orders = _orderService.GetAll().ToList();
            //for (int i = 0; i < orders.Count(); i++)
            //{
            //    try
            //    {
            //        orders[i].CustomerName = _userManager.Users.Where(u => u.Id == orders[i].CustomerId).FirstOrDefault().FirstName;
            //    }
            //    catch
            //    {
            //        orders[i].CustomerName = "unknown";
            //    }
            //}
            return View(new OrderListViewModel
            {
                Orders = orders
                    .OrderBy(c => c.OrderId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PageViewModel = new PageViewModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = orders.Count()
                }
            });
        }
    }
}
