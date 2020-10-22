using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diplom.Models.BasketModels;
using Diplom.Models.ViewModels.Shop.Pentie;
using Diplom.Models.ViewModels.ShopCart;
using Diplom.Services.Interfaces;
using Diplom.Services.Models.Shop.Penties;
using Diplom.Services.Models.ShopCart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Diplom.Controllers
{
    public class CartController : Controller
    {
        private readonly IService<PentieDTO> _pentieService;
        private readonly IService<PCategoryDTO> _categoryService;
        private readonly IService<PBrandDTO> _brandService;
        
        private readonly IService<OrderDTO> _orderService;
        private readonly IService<CountryDTO> _countryService;
        private readonly IService<AddressDTO> _addressService;

        private readonly Basket _basket;

        public CartController(IService<PentieDTO> pentieService,
                              IService<PCategoryDTO> categoryService,
                              IService<PBrandDTO> brandService,                              
                              IService<OrderDTO> orderService,
                              IService<CountryDTO> countryService,
                              IService<AddressDTO> addressService,
                              Basket basket)
        {
            _pentieService = pentieService;
            _categoryService = categoryService;
            _brandService = brandService;            
            _orderService = orderService;
            _countryService = countryService;
            _addressService = addressService;
            _basket = basket;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new BasketViewModel
            {
                Basket = _basket,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public RedirectToActionResult AddToBasket(int itemId, string returnUrl)
        {
            var pentie = _pentieService.Get(itemId);
            if (pentie != null)
            {
                var _pentie = new PentieViewModel
                {
                    PentieId = pentie.PentieId,
                    Name = pentie.Name,
                    Price = pentie.Price,
                    Discription = pentie.Discription,
                    PCategoryId = pentie.PCategoryId,
                    PCategoryName = _categoryService.GetAll()
                                  .FirstOrDefault(x => x.PCategoryId == pentie.PCategoryId).Name,
                    PBrandId = pentie.PBrandId,
                    PBrandName = _brandService.GetAll()
                                .FirstOrDefault(x => x.PBrandId == pentie.PBrandId).Name                    
                };
                _basket.AddItem(_pentie, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromBasket(int id, string returnUrl = null)
        {
            var product = _pentieService.Get(id);
            if (product != null)
            {
                var _product = new PentieViewModel
                {
                    PentieId = product.PentieId,
                    Name = product.Name,
                    Price = product.Price,
                    Discription = product.Discription,
                    PCategoryId = product.PCategoryId,
                    PCategoryName = _categoryService.GetAll()
                                   .FirstOrDefault(x => x.PCategoryId == product.PCategoryId).Name,
                    PBrandId = product.PBrandId,
                    PBrandName = _brandService.GetAll()
                                   .FirstOrDefault(x => x.PBrandId == product.PBrandId).Name                    
                };
                _basket.RemoveItem(_product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        /*=====================================================================================================================================*/
        // Методы работы с моделями Стрн и Заказов

        public ActionResult CreateCountry()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCountry(CountryDTO _country)
        {
            CountryDTO country = new CountryDTO();
            if (ModelState.IsValid)
            {
                if (_countryService.GetAll().Where(x => x.Name == _country.Name).Count() > 0)
                {
                    return RedirectToAction("Error", new { exeption = "The country already exists" });
                }
                else
                {
                    country.Name = _country.Name;
                    _countryService.Add(country);
                    _countryService.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_country);
        }

        public ActionResult DeleteCountry(int id)
        {
            var country = _countryService.Get(id);
            if (country == null)
            {
                return RedirectToAction("Error", new { exeption = "The country is not found" });
            }
            var _country = new CountryDTO
            {
                CountryId = country.CountryId,
                Name = country.Name
            };
            return View(_country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCountry(int id, IFormCollection collection)
        {
            var country = _countryService.Get(id);
            if (_addressService.GetAll().Where(x => x.CountryName == country.Name).Count() > 0)
            {
                return RedirectToAction("Error", new { exeption = "There are addresses saved for the country you are trying to delete" });
            }
            else
            {
                _countryService.Delete(country);
                _countryService.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult CreateAddress()
        {
            ViewData["CountryId"] = new SelectList(_countryService.GetAll(), "CountryId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAddress(AddressDTO _address)
        {
            AddressDTO address = new AddressDTO();
            if (ModelState.IsValid)
            {
                if (_addressService.GetAll().Where(x => x.AddressLine == _address.AddressLine).Count() > 0)
                {
                    return RedirectToAction("Error", new { exeption = "The address already exists" });
                }
                else
                {
                    address.AddressLine = _address.AddressLine;
                    address.ContactName = _address.ContactName;
                    address.CountryId = _address.CountryId;
                    address.City = _address.City;
                    _addressService.Add(address);
                    _addressService.Save();
                    return RedirectToAction(nameof(CreateAddress));
                }
            }
            return View(_address);
        }

        public ActionResult DeleteAddress(int id)
        {
            var address = _addressService.Get(id);
            if (address == null)
            {
                return RedirectToAction("Error", new { exeption = "There are orders with address you are trying to delete" });

            }
            var _address = new AddressDTO
            {
                AddressId = address.AddressId,
                AddressLine = address.AddressLine,
                ContactName = address.ContactName,
                City = address.City,
                CountryName = address.ContactName
            };

            return View(_address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAddress(int id, IFormCollection collection)
        {
            var address = _addressService.Get(id);
            if (_orderService.GetAll().Where(x => x.AddressLine == address.AddressLine).Count() > 0)
            {
                return RedirectToAction("Error", new { exeption = "There are orders with address you are trying to delete" });
            }
            else
            {
                _addressService.Delete(address);
                _addressService.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string exeption)
        {
            return View((object)exeption);
        }
    }
}
