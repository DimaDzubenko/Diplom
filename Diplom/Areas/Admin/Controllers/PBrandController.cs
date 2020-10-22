using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diplom.Helpers;
using Diplom.Models.ViewModels;
using Diplom.Models.ViewModels.Shop.Pentie;
using Diplom.Services.Interfaces;
using Diplom.Services.Models.Shop.Penties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class PBrandController : Controller
    {

        readonly IService<PBrandDTO> _brandService;
        readonly IService<PentieDTO> _pentieService;
        public int PageSize = 10;

        public PBrandController(IService<PBrandDTO> brandService,
                                IService<PentieDTO> pentieService)
        {
            _brandService = brandService;
            _pentieService = pentieService;
        }

        public ActionResult Index(int page = 1)
        {
            var brands = _brandService.GetAll();

            return View(new PBrandsListViewModel
            {
                PBrands = brands
                    .OrderBy(c => c.PBrandId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PageViewModel = new PageViewModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = brands.Count()
                }
            });
        }

        public ActionResult Details(int id)
        {
            var brand = _brandService.Get(id);
            if (brand == null)
            {
                return NotFound();
            }
            var _brand = new PBrandViewModel
            {
                PBrandId = brand.PBrandId,
                Name = brand.Name
            };
            ViewBag.Image = brand.Image;

            return View(_brand);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PBrandViewModel _brand)
        {
            PBrandDTO brand = new PBrandDTO();
            if (ModelState.IsValid)
            {
                if (_brandService.GetAll().Where(x => x.Name == _brand.Name).Count() > 0)
                {
                    return RedirectToAction("Error", new { exeption = "The brand already exist!" });
                }
                else
                {
                    brand.Name = _brand.Name;
                    brand.Image = _brand.Image != null ? ImageConverter.GetBytes(_brand.Image) : null;
                    _brandService.Add(brand);
                    _brandService.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_brand);
        }

        public ActionResult Edit(int id)
        {
            var brand = _brandService.Get(id);

            if (brand == null)
            {
                return NotFound();
            }

            var _brand = new PBrandViewModel
            {
                Name = brand.Name
            };
            ViewBag.Image = brand.Image;
            return View(_brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PBrandViewModel _brand)
        {
            var brand = _brandService.Get(id);
            if (ModelState.IsValid)
            {
                try
                {
                    brand.Name = _brand.Name;
                    brand.Image = _brand.Image != null ? ImageConverter.GetBytes(_brand.Image) : brand.Image;
                    _brandService.Update(brand);
                    _brandService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_brand);
        }

        public ActionResult Delete(int id)
        {
            var brand = _brandService.Get(id);
            if (brand == null)
            {
                return NotFound();
            }
            var _brand = new PBrandViewModel
            {
                PBrandId = brand.PBrandId,
                Name = brand.Name
            };

            ViewBag.Image = brand.Image;
            return View(_brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var brand = _brandService.Get(id);
            if (_pentieService.GetAll().Where(x => x.PBrandName == brand.Name).Count() > 0)
            {
                return RedirectToAction("Error", new { exeption = "There are products of the brand you are trying to delete" });
            }
            else
            {
                _brandService.Delete(brand);
                _brandService.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string exeption)
        {
            return View((object)exeption);
        }
    }
}
