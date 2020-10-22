using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diplom.Models.ViewModels;
using Diplom.Models.ViewModels.Blog;
using Diplom.Services.Interfaces;
using Diplom.Services.Models.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class TagController : Controller
    {
        IService<TagDTO> _tagService;
        IService<PostTagDTO> _postTagService;

        public int PageSize = 8;

        public TagController(IService<TagDTO> tagService,
                                  IService<PostTagDTO> postTagService)
        {
            _tagService = tagService;
            _postTagService = postTagService;
        }

        public ActionResult Index(int page = 1)
        {
            var tags = _tagService.GetAll();

            return View(new TagListViewModel
            {
                Tags = tags
                    .OrderBy(c => c.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PageViewModel = new PageViewModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = tags.Count()
                }
            });
        }

        public ActionResult Details(int id = 1)
        {
            TagDTO tag = null;
            try
            {
                tag = _tagService.Get(id);
            }
            catch
            {
                return RedirectToAction("Error", new { exeption = "The style doesn't exist anymore!" });
            }
            if (tag == null)
            {
                return NotFound();
            }
            var _tag = new TagViewModel
            {
                Id = tag.Id,
                Title = tag.Title
            };
            return View(_tag);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TagViewModel _tag)
        {
            TagDTO tag = new TagDTO();
            if (ModelState.IsValid)
            {
                if (_tagService.GetAll().Where(x => x.Title == _tag.Title).Count() > 0)
                {
                    return RedirectToAction("Error", new { exeption = "The style already exist!" });
                }
                else
                {
                    tag.Title = _tag.Title;                   
                    _tagService.Add(tag);
                    _tagService.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_tag);
        }

        public ActionResult Edit(int id)
        {
            var tag = _tagService.Get(id);

            if (tag == null)
            {
                return NotFound();
            }

            var _tag = new TagViewModel
            {
                Title = tag.Title                
            };            
            return View(_tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TagViewModel _tag)
        {
            var tag = _tagService.Get(id);
            if (ModelState.IsValid)
            {
                try
                {
                    tag.Title = _tag.Title;                    
                    _tagService.Update(tag);
                    _tagService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_tag);
        }

        public ActionResult Delete(int id)
        {
            var tag = _tagService.Get(id);
            if (tag == null)
            {
                return NotFound();
            }
            var _tag = new TagViewModel
            {
                Id = tag.Id,
                Title = tag.Title
            };

            return View(_tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var tag = _tagService.Get(id);
            if (_tagService.GetAll().Where(x => x.Title == tag.Title).Count() > 0)
                return RedirectToAction(nameof(Error));
            else
            {
                _tagService.Delete(tag);
                _tagService.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string exeption)
        {
            return View((object)exeption);
        }
    }
}
