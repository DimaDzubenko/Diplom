using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class PentieColorController : Controller
    {
        IService<PentieColorDTO> _pentieColorService;
        IService<PentieDTO> _pentieService;
        IService<PColorDTO> _colorService;

        public int PageSize = 6;

        public PentieColorController(IService<PentieColorDTO> pentieColorService,
                                     IService<PentieDTO> pentieService,
                                     IService<PColorDTO> colorService)
        {
            _pentieColorService = pentieColorService;
            _pentieService = pentieService;
            _colorService = colorService;
        }

        public ActionResult Index(string pentie
                                , string color
                                , string searchString
                                , int page = 1
                                , int PageSize = 12
                                , int sort = 1)
        {

            var pentieColors = _pentieColorService.GetAll();

            var predicate = PredicateBuilder.New<PentieColorDTO>(true);

            if (!string.IsNullOrEmpty(searchString))
            {
                predicate = predicate.And(x => x.PentieName.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!string.IsNullOrEmpty(pentie))
            {
                predicate = predicate.And(i => i.PentieName == pentie);
            }

            if (!string.IsNullOrEmpty(color))
            {
                predicate = predicate.And(i => i.ColorName == color);
            }

            IEnumerable<PentieColorDTO> selectedPentieColors;

            selectedPentieColors = pentieColors.Where(predicate).Select(i => i);
            var total = selectedPentieColors.Count();

            selectedPentieColors = sort > 1 ? selectedPentieColors.OrderByDescending(p => p.ColorName) : selectedPentieColors.OrderBy(p => p.PentieName);

            var pentieColorListModel = new PentieColorListViewModel
            {
                PentieColors = selectedPentieColors
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PageViewModel = new PageViewModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = total
                },
                CurrentPentie = pentie,
                CurrentColor = color
            };
            ViewBag.CurrentPentie = pentieColorListModel.CurrentPentie;
            ViewBag.CurrentColor = pentieColorListModel.CurrentColor;
            ViewBag.TotalCount = total;
            ViewBag.SortBy = sort;
            ViewBag.ShowingCount = total < PageSize ? total : PageSize;           

            return View(pentieColorListModel);
        }

        public ActionResult Details(int id)
        {
            var pentieColor = _pentieColorService.Get(id);
            if (pentieColor == null)
            {
                return NotFound();
            }
            var _pentieColor = new PentieColorViewModel
            {
                Id = pentieColor.Id,
                PentieId = pentieColor.PentieId,
                PentieName = _pentieService.GetAll().FirstOrDefault(x => x.PentieId == pentieColor.PentieId).Name,
                ColorId = pentieColor.ColorId,
                ColorName = _colorService.GetAll().FirstOrDefault(x => x.PColorId == pentieColor.ColorId).Name
            };

            return View(_pentieColor);
        }


        public ActionResult Create()
        {
            ViewData["PentieId"] = new SelectList(_pentieService.GetAll(), "PentieId", "Name");
            ViewData["ColorId"] = new SelectList(_colorService.GetAll(), "ColorId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PentieColorViewModel _pentieColor)
        {
            PentieColorDTO pentieColor = new PentieColorDTO();
            if (ModelState.IsValid)
            {
                if (_pentieColorService.GetAll().Where(x => x.Id == _pentieColor.Id).Count() > 0)
                {
                    return RedirectToAction("Error", new { exeption = "The tattoo already exist!" });
                }
                else
                {
                    pentieColor.PentieId = _pentieColor.PentieId;
                    pentieColor.PentieName = _pentieService.GetAll().FirstOrDefault(x => x.PentieId == _pentieColor.PentieId).Name;
                    pentieColor.ColorId = _pentieColor.ColorId;
                    pentieColor.ColorName = _colorService.GetAll().FirstOrDefault(x => x.PColorId == _pentieColor.ColorId).Name;
                    _pentieColorService.Add(pentieColor);
                    _pentieColorService.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_pentieColor);
        }

        public ActionResult Edit(int id)
        {
            var pentieColor = _pentieColorService.Get(id);

            if (pentieColor == null)
            {
                return NotFound();
            }

            var _pentieColor = new PentieColorViewModel()
            {
                Id = pentieColor.Id,
                PentieName = pentieColor.PentieName,
                PentieId = pentieColor.PentieId,
                ColorName = pentieColor.ColorName,
                ColorId = pentieColor.ColorId
            };

            ViewData["Penties"] = new SelectList(_pentieService.GetAll(), "Penties", "Name");
            ViewData["Colors"] = new SelectList(_colorService.GetAll(), "Colors", "Name");
            return View(_pentieColor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PentieColorViewModel _pentieColor)
        {
            var pentieColor = _pentieColorService.Get(id);
            if (ModelState.IsValid)
            {
                try
                {
                    pentieColor.PentieName = _pentieColor.PentieName;
                    pentieColor.PentieId = _pentieColor.PentieId;
                    pentieColor.ColorName = _pentieColor.ColorName;
                    pentieColor.ColorId = _pentieColor.ColorId;

                    _pentieColorService.Update(pentieColor);
                    _pentieColorService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_pentieColor);
        }

        public ActionResult Delete(int id)
        {
            var pentieColor = _pentieColorService.Get(id);
            if (pentieColor == null)
            {
                return NotFound();
            }
            var _pentieColor = new PentieColorViewModel
            {
                PentieName = _pentieService.GetAll().FirstOrDefault(x => x.PentieId == pentieColor.PentieId).Name,
                PentieId = pentieColor.PentieId,
                ColorName = _colorService.GetAll().FirstOrDefault(x => x.PColorId == pentieColor.ColorId).Name,
                ColorId = pentieColor.ColorId
            };

            return View(_pentieColor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var pentieColor = _pentieColorService.Get(id);
            _pentieColorService.Delete(pentieColor);
            _pentieColorService.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string exeption)
        {
            return View((object)exeption);
        }
    }
}
