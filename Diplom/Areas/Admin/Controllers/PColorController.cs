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
    public class PColorController : Controller
    {
        readonly IService<PColorDTO> _colorService;
        readonly IService<PentieDTO> _pentieService;

        public int PageSize { get; set; } = 8;

        public PColorController(IService<PColorDTO> colorService,
                                IService<PentieDTO> pentieService)
        {
            _colorService = colorService;
            _pentieService = pentieService;
        }


        public ActionResult Index(int page = 1)
        {
            var colors = _colorService.GetAll();

            return View(new PColorListViewModel
            {
                PColors = colors
                    .OrderBy(c => c.PColorId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PageViewModel = new PageViewModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = colors.Count()
                }
            });
        }

        public ActionResult Details(int id)
        {
            var color = _colorService.Get(id);
            if (color == null)
            {
                return NotFound();
            }
            var _color = new PColorViewModel
            {
                PColorId = color.PColorId,
                Name = color.Name
            };
            return View(_color);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PColorViewModel _color)
        {
            PColorDTO color = new PColorDTO();
            if (ModelState.IsValid)
            {
                if (_colorService.GetAll().Where(x => x.Name == _color.Name).Count() > 0)
                {
                    return RedirectToAction("Error", new { exeption = "The category already exist!" });
                }
                else
                {
                    color.Name = _color.Name;
                    _colorService.Add(color);
                    _colorService.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_color);
        }

        public ActionResult Edit(int id)
        {
            var color = _colorService.Get(id);

            if (color == null)
            {
                return NotFound();
            }

            var _category = new PColorViewModel
            {
                Name = color.Name
            };

            return View(_category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PColorViewModel _color)
        {
            var color = _colorService.Get(id);
            if (ModelState.IsValid)
            {
                try
                {
                    color.Name = _color.Name;
                    _colorService.Update(color);
                    _colorService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_color);
        }

        public ActionResult Delete(int id)
        {
            var color = _colorService.Get(id);
            if (color == null)
            {
                return NotFound();
            }
            var _color = new PColorViewModel
            {
                PColorId = color.PColorId,
                Name = color.Name
            };

            return View(_color);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var color = _colorService.Get(id);
            if (_pentieService.GetAll().Where(x => x.PentieId == color.PColorId).Count() > 0)
            {
                return RedirectToAction("Error", new { exeption = "There are products in category you are trying to delete" });
            }
            else
            {
                _colorService.Delete(color);
                _colorService.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string exeption)
        {
            return View((object)exeption);
        }
    }
}
