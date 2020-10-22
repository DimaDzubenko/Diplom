using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diplom.Models.ViewModels;
using Diplom.Models.ViewModels.Shop.Pentie;
using Diplom.Services.Interfaces;
using Diplom.Services.Models.Shop.Penties;
using LinqKit;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    public class ProductController : Controller
    {
        IService<PentieDTO> _productService;
        IService<PCategoryDTO> _categoryService;
        IService<PBrandDTO> _brandService;

        public ProductController(IService<PentieDTO> productService,
                                  IService<PCategoryDTO> categoryService,
                                  IService<PBrandDTO> brandService)
                                 
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;            
        }

        public ActionResult Index(string category,
                                 string brand,
                                 string searchString,
                                 int page = 1,
                                 int PageSize = 12,
                                 int sort = 1)
        {
            var products = _productService.GetAll();

            var predicate = PredicateBuilder.New<PentieDTO>(true);

            if (!string.IsNullOrEmpty(searchString))
            {
                predicate = predicate.And(x => x.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!string.IsNullOrEmpty(category))
            {
                predicate = predicate.And(i => i.PCategoryName == category);
            }

            if (!string.IsNullOrEmpty(brand))
            {
                predicate = predicate.And(i => i.PBrandName == brand);
            }

            IEnumerable<PentieDTO> selectedProducts;

            selectedProducts = products.Where(predicate).Select(i => i);
            var total = selectedProducts.Count();
            selectedProducts = sort > 1 ? selectedProducts.OrderByDescending(p => p.Price) : selectedProducts.OrderBy(p => p.Name);

            var productListModel = new PentieListViewModel
            {
                Penties = selectedProducts
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize),
                PageViewModel = new PageViewModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = total
                },
                CurrentCategory = category,
                CurrentBrand = brand
            };
            ViewBag.CurrentCategory = productListModel.CurrentCategory;
            ViewBag.CurrentBrand = productListModel.CurrentBrand;
            ViewBag.SortBy = sort;
            ViewBag.TotalCount = total;
            ViewBag.ShowingCount = total < PageSize ? total : PageSize;

            return View(productListModel);
        }

        public ActionResult Details(int id)
        {
            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            var _product = new PentieViewModel
            {
                PentieId = product.PentieId,
                Name = product.Name,
                Price = product.Price,
                Discription = product.Discription,
                PCategoryId = product.PCategoryId,
                PCategoryName = _categoryService.GetAll().FirstOrDefault(x => x.PCategoryId == product.PCategoryId).Name,
                PBrandId = product.PBrandId,
                PBrandName = _brandService.GetAll().FirstOrDefault(x => x.PBrandId == product.PBrandId).Name
            };
            ViewBag.Image = product.Image;

            return View(_product);
        }
    }
}
