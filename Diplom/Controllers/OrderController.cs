using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diplom.Data;
using Diplom.Models.BasketModels;
using Diplom.Models.ViewModels.ShopCart;
using Diplom.Services.Interfaces;
using Diplom.Services.Models.ShopCart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Diplom.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        //UserManager<ApplicationUser> userManager;
        //SignInManager<ApplicationUser> signInManager;
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
                               //UserManager<ApplicationUser> _userManager,
                               //SignInManager<ApplicationUser> _signInManager,
                               IConfiguration configuration)
                               //IEmailSender _emailSender)
        {
            _orderService = orderService;
            _addressService = addressService;
            _countriesService = countriesService;
            _basket = basket;
            _context = context;
            //userManager = _userManager;
            //signInManager = _signInManager;
            _configuration = configuration;
            //emailSender = _emailSender;
        }

        public ViewResult Checkout() => View(new OrderViewModel());


        [HttpPost]
        public IActionResult Checkout(OrderViewModel _order)
        {
            if (_basket.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                OrderDTO order = new OrderDTO
                {
                    AddressLine = _order.AddressLine,
                    CountryId = _order.CountryId,
                    TotalValue = (int)_basket.ComputeTotalValue()
                };
            }
            else
            {
                return View(_order);
            }

            if (_basket.Items.Count() == 0)
            {
                return RedirectToAction("Error", new { exeption = "There are no products in your basket" });
            }
            if (ModelState.IsValid)
            {
                OrderDTO order = new OrderDTO
                {
                    AddressLine = _order.AddressLine,
                    CountryId = _order.CountryId,
                    TotalValue = (int)_basket.ComputeTotalValue()
                };


                if (_addressService.GetAll().Where(x => x.AddressLine == _order.AddressLine).Count() > 0)
                {
                    order.AddressId = _addressService.GetAll()
                                      .FirstOrDefault(x => x.AddressLine == _order.AddressLine).AddressId;
                }
                else
                {
                    _addressService.Add(new AddressDTO
                    {
                        AddressLine = order.AddressLine,
                        ContactName = order.CustomerName
                    });
                    order.AddressId = _addressService.GetAll()
                                      .FirstOrDefault(x => x.AddressLine == _order.AddressLine).AddressId;
                }

                _orderService.Add(order);
                _orderService.Save();

                //var email = userManager.Users.Where(u => u.Id == order.CustomerId).FirstOrDefault().Email;
                //emailSender.SendEmailOrderConfirmationAsync(email, order);

                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(_order);
            }
        }

        public ViewResult Completed()
        {
            _basket.Clear();
            return View();
        }

        /*==============================================================================================================================*/
        //public ActionResult List(int page = 1)
        //{
        //    var orders = orderService.GetAll().ToList();
        //    for (int i = 0; i < orders.Count(); i++)
        //    {
        //        try
        //        {
        //            //orders[i].CustomerName = userManager.Users.Where(u => u.Id == orders[i].CustomerId).FirstOrDefault().FirstName;
        //        }
        //        catch
        //        {
        //            orders[i].CustomerName = "unknown";
        //        }
        //    }
        //    return View(new OrderListViewModel
        //    {
        //        Orders = (IEnumerable<OrderViewModel>)orders
        //            .OrderBy(c => c.OrderId)
        //            .Skip((page - 1) * PageSize)
        //            .Take(PageSize),
        //        PageViewModel = new PageViewModel
        //        {
        //            CurrentPage = page,
        //            ItemsPerPage = PageSize,
        //            TotalItems = orders.Count()
        //        }
        //    });
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    var order = orderService.Get(id);
        //    orderService.Delete(order);
        //    orderService.Save();
        //    return RedirectToAction(nameof(List));
        //}

        //public IActionResult Checkout()
        //{
        //    ViewData["Countries"] = new SelectList(countriesService.GetAll(), "CountryId", "Name");

        //    return View(new OrderViewModelWEB());
        //}

        //public IActionResult Error(string exeption)
        //{
        //    return View((object)exeption);
        //}

        //[HttpPost]
        //public IActionResult Checkout(OrderViewModelWEB _order)
        //{
        //    if (basket.Items.Count() == 0)
        //    {
        //        return RedirectToAction("Error", new { exeption = "There are no products in your basket" });
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        OrderViewModel order = new OrderViewModel
        //        {
        //            //CustomerId = userManager.GetUserId(HttpContext.User),
        //            //CustomerName = this.User.FindFirstValue(ClaimTypes.Name),
        //            AddressLine = _order.AddressLine,
        //            CountryId = _order.CountryId,
        //            TotalValue = (int)basket.ComputeTotalValue()
        //        };


        //        if (addressService.GetAll().Where(x => x.AddressLine == _order.AddressLine).Count() > 0)
        //        {
        //            order.AddressId = addressService.GetAll()
        //                              .FirstOrDefault(x => x.AddressLine == _order.AddressLine).AddressId;
        //        }
        //        else
        //        {
        //            addressService.Add(new AddressViewModelWEB
        //            {
        //                AddressLine = order.AddressLine,
        //                ContactName = order.CustomerName
        //            });                    
        //            order.AddressId = addressService.GetAll()
        //                              .FirstOrDefault(x => x.AddressLine == _order.AddressLine).AddressId;
        //        }

        //        //orderService.Add(order);
        //        orderService.Save();

        //        //var email = userManager.Users.Where(u => u.Id == order.CustomerId).FirstOrDefault().Email;
        //        //emailSender.SendEmailOrderConfirmationAsync(email, order);

        //        return RedirectToAction(nameof(Completed));
        //    }
        //    else
        //    {
        //        return View(_order);
        //    }
        //}

        //public RedirectToActionResult Clear(string returnUrl)
        //{
        //    basket.Clear();
        //    return RedirectToAction("Index", "Cart", new { returnUrl });
        //}

        //public ViewResult Completed()
        //{
        //    basket.Clear();
        //    return View();
        //}
    }
}
