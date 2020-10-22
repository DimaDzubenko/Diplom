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
    public class PSizeController : Controller
    {

        readonly IService<PSizeDTO> _sizeService;
        readonly IService<PentieDTO> _pentieService;

        public int PageSize { get; set; } = 8;

        public PSizeController(IService<PSizeDTO> sizeService,
                               IService<PentieDTO> pentieService)
        {
            _sizeService = sizeService;
            _pentieService = pentieService;
        }

        public ActionResult Index(int page = 1)
        {
            var sizes = _sizeService.GetAll();

            return View(new PSizeListViewModel
            {
                PSizes = sizes
                    .OrderBy(c => c.PSizeId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PageViewModel = new PageViewModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = sizes.Count()
                }
            });
        }

        public ActionResult Details(int id)
        {
            var size = _sizeService.Get(id);
            if (size == null)
            {
                return NotFound();
            }
            var _size = new PSizeViewModel
            {
                PSizeId = size.PSizeId,
                Name = size.Name
            };
            return View(_size);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PSizeViewModel _size)
        {
            PSizeDTO size = new PSizeDTO();
            if (ModelState.IsValid)
            {
                if (_sizeService.GetAll().Where(x => x.Name == _size.Name).Count() > 0)
                {
                    return RedirectToAction("Error", new { exeption = "The category already exist!" });
                }
                else
                {
                    size.Name = _size.Name;
                    _sizeService.Add(size);
                    _sizeService.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_size);
        }

        public ActionResult Edit(int id)
        {
            var size = _sizeService.Get(id);

            if (size == null)
            {
                return NotFound();
            }

            var _size = new PSizeViewModel
            {
                Name = size.Name
            };

            return View(_size);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PSizeViewModel _size)
        {
            var size = _sizeService.Get(id);
            if (ModelState.IsValid)
            {
                try
                {
                    size.Name = _size.Name;
                    _sizeService.Update(size);
                    _sizeService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_size);
        }

        public ActionResult Delete(int id)
        {
            var size = _sizeService.Get(id);
            if (size == null)
            {
                return NotFound();
            }
            var _size = new PSizeViewModel
            {
                PSizeId = size.PSizeId,
                Name = size.Name
            };

            return View(_size);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var size = _sizeService.Get(id);
            if (_pentieService.GetAll().Where(x => x.PentieId == size.PSizeId).Count() > 0)
            {
                return RedirectToAction("Error", new { exeption = "There are products in category you are trying to delete" });
            }
            else
            {
                _sizeService.Delete(size);
                _sizeService.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string exeption)
        {
            return View((object)exeption);
        }
    }
}
