using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class PCategoryController : Controller
    {
        readonly IService<PCategoryDTO> _categoryService;
        readonly IService<PentieDTO> _pentieService;

        public int PageSize { get; set; } = 8;

        public PCategoryController(IService<PCategoryDTO> categoryService,
                                    IService<PentieDTO> pentieService)
        {
            _categoryService = categoryService;
            _pentieService = pentieService;
        }

        public ActionResult Index(int page = 1)
        {
            var categories = _categoryService.GetAll();

            return View(new PCategoriesListViewModel
            {
                PCategories = categories
                    .OrderBy(c => c.PCategoryId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PageViewModel = new PageViewModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = categories.Count()
                }
            });
        }

        public ActionResult Details(int id)
        {
            var category = _categoryService.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            var _category = new PCategoryViewModel
            {
                PCategoryId = category.PCategoryId,
                Name = category.Name
            };
            return View(_category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PCategoryViewModel _category)
        {
            PCategoryDTO category = new PCategoryDTO();
            if (ModelState.IsValid)
            {
                if (_categoryService.GetAll().Where(x => x.Name == _category.Name).Count() > 0)
                {
                    return RedirectToAction("Error", new { exeption = "The category already exist!" });
                }
                else
                {
                    category.Name = _category.Name;
                    _categoryService.Add(category);
                    _categoryService.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_category);
        }

        public ActionResult Edit(int id)
        {
            var category = _categoryService.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            var _category = new PCategoryViewModel
            {
                Name = category.Name
            };

            return View(_category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PCategoryViewModel _category)
        {
            var category = _categoryService.Get(id);
            if (ModelState.IsValid)
            {
                try
                {
                    category.Name = _category.Name;
                    _categoryService.Update(category);
                    _categoryService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_category);
        }

        public ActionResult Delete(int id)
        {
            var category = _categoryService.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            var _category = new PCategoryViewModel
            {
                PCategoryId = category.PCategoryId,
                Name = category.Name
            };

            return View(_category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var category = _categoryService.Get(id);
            if (_pentieService.GetAll().Where(x => x.PCategoryName == category.Name).Count() > 0)
            {
                return RedirectToAction("Error", new { exeption = "There are products in category you are trying to delete" });
            }
            else
            {
                _categoryService.Delete(category);
                _categoryService.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string exeption)
        {
            return View((object)exeption);
        }
    }
}
