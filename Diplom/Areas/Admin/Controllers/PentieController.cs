using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diplom.Helpers;
using Diplom.Models.ViewModels;
using Diplom.Models.ViewModels.Shop.Pentie;
using Diplom.Services.Interfaces;
using Diplom.Services.Models.Shop.Penties;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class PentieController : Controller
    {
        readonly IService<PentieDTO> _pentieService;
        readonly IService<PBrandDTO> _brandService;
        readonly IService<PCategoryDTO> _categoryService;
        readonly IService<PentieColorDTO> _pentieColorService;
        readonly IService<PentieSizeDTO> _pentieSizeService;

        public int PageSize { get; set; } = 8;

        public PentieController(IService<PentieDTO> pentieService,
                                IService<PBrandDTO> brandService,
                                IService<PCategoryDTO> categoryService,
                                IService<PentieColorDTO> pentieColorService,
                                IService<PentieSizeDTO> pentieSizeService)
        {
            _pentieService = pentieService;
            _brandService = brandService;
            _categoryService = categoryService;
            _pentieColorService = pentieColorService;
            _pentieSizeService = pentieSizeService;

        }
        public ActionResult Index(string category,
                                 string brand,
                                 string searchString,
                                 int page = 1,
                                 int PageSize = 12,
                                 int sort = 1)
        {
            var penties = _pentieService.GetAll();

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

            IEnumerable<PentieDTO> selectedPenties;

            selectedPenties = penties.Where(predicate).Select(i => i);
            var total = selectedPenties.Count();
            selectedPenties = sort > 1 ? selectedPenties.OrderByDescending(p => p.Price) : selectedPenties.OrderBy(p => p.Name);

            var pentieListModel = new PentieListViewModel
            {
                Penties = selectedPenties
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
            ViewBag.CurrentCategory = pentieListModel.CurrentCategory;
            ViewBag.CurrentBrand = pentieListModel.CurrentBrand;
            ViewBag.SortBy = sort;
            ViewBag.TotalCount = total;
            ViewBag.ShowingCount = total < PageSize ? total : PageSize;

            return View(pentieListModel);
        }

        public ActionResult Details(int id)
        {
            var pentie = _pentieService.Get(id);
            if (pentie == null)
            {
                return NotFound();
            }
            var _pentie = new PentieViewModel
            {
                PentieId = pentie.PentieId,
                Name = pentie.Name,
                Discription = pentie.Discription,
                Price = pentie.Price,
                PCategoryId = pentie.PCategoryId,
                PCategoryName = _categoryService.GetAll().FirstOrDefault(x => x.PCategoryId == pentie.PCategoryId).Name,
                PBrandId = pentie.PBrandId,
                PBrandName = _brandService.GetAll().FirstOrDefault(x => x.PBrandId == pentie.PBrandId).Name
            };
            ViewBag.Image = pentie.Image;
            return View(_pentie);
        }

        public ActionResult Create()
        {
            ViewData["PBrandId"] = new SelectList(_brandService.GetAll(), "PBrandId", "Name");
            ViewData["PCategoryId"] = new SelectList(_categoryService.GetAll(), "PCategoryId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PentieViewModel _pentie)
        {
            PentieDTO pentie = new PentieDTO();
            if (ModelState.IsValid)
            {
                if (_pentieService.GetAll().Where(x => x.Name == _pentie.Name).Count() > 0)
                {
                    return RedirectToAction("Error", new { exeption = "The product already exist!" });
                }
                else
                {
                    pentie.Name = _pentie.Name;
                    pentie.Discription = _pentie.Discription;
                    pentie.Price = _pentie.Price;                    
                    pentie.PCategoryId = _pentie.PCategoryId;
                    pentie.PBrandId = _pentie.PBrandId;
                    pentie.Image = _pentie.Image != null ? ImageConverter.GetBytes(_pentie.Image) : null;
                    _pentieService.Add(pentie);
                    _pentieService.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_pentie);
        }


        public ActionResult Edit(int id)
        {
            var pentie = _pentieService.Get(id);

            if (pentie == null)
            {
                return NotFound();
            }
            var _pentie = new PentieViewModel
            {
                PentieId = pentie.PentieId,
                Name = pentie.Name,
                Discription = pentie.Discription,
                Price = pentie.Price,
                PCategoryId = pentie.PCategoryId,
                PBrandId = pentie.PBrandId
            };

            ViewBag.Image = pentie.Image;
            ViewData["Brands"] = new SelectList(_brandService.GetAll(), "PBrandId", "Name");
            ViewData["Categories"] = new SelectList(_categoryService.GetAll(), "PCategoryId", "Name");

            return View(_pentie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PentieViewModel _pentie)
        {
            var pentie = _pentieService.Get(id);
            if (ModelState.IsValid)
            {
                try
                {
                    pentie.Name = _pentie.Name;
                    pentie.Discription = _pentie.Discription;
                    pentie.Price = _pentie.Price;
                    pentie.PCategoryId = _pentie.PCategoryId;
                    pentie.PBrandId = _pentie.PBrandId;
                    pentie.Image = _pentie.Image != null ? ImageConverter.GetBytes(_pentie.Image) : pentie.Image;
                    _pentieService.Update(pentie);
                    _pentieService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_pentie);
        }


        public ActionResult Delete(int id)
        {
            var pentie = _pentieService.Get(id);
            if (pentie == null)
            {
                return NotFound();
            }
            var _pentie = new PentieViewModel
            {
                PentieId = pentie.PentieId,
                Name = pentie.Name,
                Discription = pentie.Discription,
                Price = pentie.Price,
                PCategoryName = _categoryService.GetAll().Where(x => x.PCategoryId == pentie.PCategoryId).FirstOrDefault().Name,
                PBrandName = _brandService.GetAll().Where(x => x.PBrandId == pentie.PBrandId).FirstOrDefault().Name
            };
            ViewBag.Image = pentie.Image;

            return View(_pentie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var pentie = _pentieService.Get(id);
            _pentieService.Delete(pentie);
            _pentieService.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string exeption)
        {
            return View((object)exeption);
        }
    }
}
