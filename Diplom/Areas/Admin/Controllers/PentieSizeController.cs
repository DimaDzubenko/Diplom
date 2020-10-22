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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class PentieSizeController : Controller
    {
        IService<PentieSizeDTO> _pentieSizeService;
        IService<PentieDTO> _pentieService;
        IService<PSizeDTO> _sizeService;

        public int PageSize = 6;

        public PentieSizeController(IService<PentieSizeDTO> pentieSizeService,
                                    IService<PentieDTO> pentieService,
                                    IService<PSizeDTO> sizeService)
        {
            _pentieSizeService = pentieSizeService;
            _pentieService = pentieService;
            _sizeService = sizeService;
        }

        public ActionResult Index(int page = 1)
        {
            var pentieSizes = _pentieSizeService.GetAll();

            return View(new PentieSizeListViewModel
            {
                PentieSizes = pentieSizes
                    .OrderBy(c => c.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PageViewModel = new PageViewModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = pentieSizes.Count()
                }
            });
        }

        public ActionResult Details(int id)
        {
            var pentieSizes = _pentieSizeService.Get(id);
            if (pentieSizes == null)
            {
                return NotFound();
            }
            var _pentieSizes = new PentieSizeViewModel
            {
                Id = pentieSizes.Id,
                PentieId = pentieSizes.PentieId,
                PentieName = _pentieService.GetAll().FirstOrDefault(x => x.PentieId == pentieSizes.PentieId).Name,
                SizeId = pentieSizes.SizeId,
                SizeName = _sizeService.GetAll().FirstOrDefault(x => x.PSizeId == pentieSizes.SizeId).Name
            };
            return View(_pentieSizes);
        }

        public ActionResult Create()
        {
            ViewData["PentieId"] = new SelectList(_pentieService.GetAll(), "PentieId", "Name");
            ViewData["SizeId"] = new SelectList(_sizeService.GetAll(), "PSizeId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PentieSizeViewModel _pentieSizes)
        {
            PentieSizeDTO pentieSizes = new PentieSizeDTO();
            if (ModelState.IsValid)
            {
                if (_pentieSizeService.GetAll().Where(x => x.Id == _pentieSizes.Id).Count() > 0)
                {
                    return RedirectToAction("Error", new { exeption = "The tattoo already exist!" });
                }
                else
                {
                    pentieSizes.PentieName = _pentieService.GetAll().FirstOrDefault(x => x.PentieId == pentieSizes.PentieId).Name;
                    pentieSizes.PentieId = _pentieSizes.PentieId;
                    pentieSizes.SizeName = _sizeService.GetAll().FirstOrDefault(x => x.PSizeId == pentieSizes.SizeId).Name;
                    pentieSizes.SizeId = _pentieSizes.SizeId;
                    _pentieSizeService.Add(pentieSizes);
                    _pentieSizeService.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_pentieSizes);
        }

        public ActionResult Edit(int id)
        {
            var pentieSizes = _pentieSizeService.Get(id);

            if (pentieSizes == null)
            {
                return NotFound();
            }

            var _pentieSizes = new PentieSizeViewModel()
            {
                PentieName = pentieSizes.PentieName,
                PentieId = pentieSizes.PentieId,
                SizeName = pentieSizes.SizeName,
                SizeId = pentieSizes.SizeId
            };

            ViewData["Styles"] = new SelectList(_pentieService.GetAll(), "PentieId", "Name");
            ViewData["Authors"] = new SelectList(_sizeService.GetAll(), "PSizeId", "Name");
            return View(_pentieSizes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PentieSizeViewModel _pentieSizes)
        {
            var pentieSizes = _pentieSizeService.Get(id);
            if (ModelState.IsValid)
            {
                try
                {
                    pentieSizes.PentieName = _pentieSizes.PentieName;
                    pentieSizes.PentieId = _pentieSizes.PentieId;
                    pentieSizes.SizeName = _pentieSizes.SizeName;
                    pentieSizes.SizeId = _pentieSizes.SizeId;
                    _pentieSizeService.Update(pentieSizes);
                    _pentieSizeService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_pentieSizes);
        }

        public ActionResult Delete(int id)
        {
            var pentieSizes = _pentieSizeService.Get(id);
            if (pentieSizes == null)
            {
                return NotFound();
            }
            var _pentieSizes = new PentieSizeViewModel
            {
                PentieName = _pentieService.GetAll().FirstOrDefault(x => x.PentieId == pentieSizes.PentieId).Name,
                PentieId = pentieSizes.PentieId,
                SizeName = _sizeService.GetAll().FirstOrDefault(x => x.PSizeId == pentieSizes.SizeId).Name,
                SizeId = pentieSizes.SizeId
            };

            return View(_pentieSizes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var pentieSizes = _pentieSizeService.Get(id);
            _pentieSizeService.Delete(pentieSizes);
            _pentieSizeService.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string exeption)
        {
            return View((object)exeption);
        }
    }
}
