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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class PostTagController : Controller
    {
        IService<PostTagDTO> _postTagService;
        IService<PostDTO> _postService;
        IService<TagDTO> _tagService;

        public int PageSize = 6;

        public PostTagController(IService<PostTagDTO> postTagService,
                                 IService<PostDTO> postService,
                                 IService<TagDTO> tagService)
        {
            _postTagService = postTagService;
            _postService = postService;
            _tagService = tagService;
        }


        public ActionResult Index(int page = 1)
        {
            var postTags = _postTagService.GetAll();

            return View(new PostTagListViewModel
            {
                PostTags = postTags
                    .OrderBy(c => c.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PageViewModel = new PageViewModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = postTags.Count()
                }
            });
        }

        public ActionResult Details(int id)
        {
            var postTag = _postTagService.Get(id);
            if (postTag == null)
            {
                return NotFound();
            }
            var _postTag = new PostTagViewModel
            {
                Id = postTag.Id,                              
                PostId = postTag.PostId,
                PostName = _postService.GetAll().FirstOrDefault(x => x.Id == postTag.Id).Title,
                TagId = postTag.TagId,
                TagName = _tagService.GetAll().FirstOrDefault(x => x.Id == postTag.Id).Title
            };
            
            return View(_postTag);
        }

        public ActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_postService.GetAll(), "Id", "Title");
            ViewData["TagId"] = new SelectList(_tagService.GetAll(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostTagViewModel _postTag)
        {
            PostTagDTO postTag = new PostTagDTO();
            if (ModelState.IsValid)
            {
                if (_postTagService.GetAll().Where(x => x.Id == _postTag.Id).Count() > 0)
                {
                    return RedirectToAction("Error", new { exeption = "The tattoo already exist!" });
                }
                else
                {
                    postTag.PostId = _postTag.PostId;
                    postTag.PostName = _postService.GetAll().FirstOrDefault(x => x.Id == postTag.Id).Title;
                    postTag.TagId = _postTag.TagId;
                    postTag.TagName = _tagService.GetAll().FirstOrDefault(x => x.Id == postTag.Id).Title;
                    _postTagService.Add(postTag);
                    _postTagService.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_postTag);
        }

        public ActionResult Edit(int id)
        {
            var postTag = _postTagService.Get(id);

            if (postTag == null)
            {
                return NotFound();
            }

            var _postTag = new PostTagViewModel()
            {
                PostId = postTag.PostId,
                PostName = postTag.PostName,
                TagId = postTag.TagId,
                TagName = postTag.TagName
            };

            ViewData["Posts"] = new SelectList(_postService.GetAll(), "Id", "Title");
            ViewData["Tags"] = new SelectList(_tagService.GetAll(), "Id", "Title");
            return View(_postTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PostTagViewModel _postTag)
        {
            var postTag = _postTagService.Get(id);
            if (ModelState.IsValid)
            {
                try
                {
                    postTag.PostId = _postTag.PostId;
                    postTag.PostName = _postService.GetAll().FirstOrDefault(x => x.Id == postTag.Id).Title;
                    postTag.TagId = _postTag.TagId;
                    postTag.TagName = _tagService.GetAll().FirstOrDefault(x => x.Id == postTag.Id).Title;
                    _postTagService.Update(postTag);
                    _postTagService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_postTag);
        }

        public ActionResult Delete(int id)
        {
            var postTag = _postTagService.Get(id);
            if (postTag == null)
            {
                return NotFound();
            }
            var _postTag = new PostTagViewModel
            {                
                PostId = postTag.Id,
                PostName = _postService.GetAll().FirstOrDefault(x => x.Id == postTag.Id).Title,
                TagId = postTag.Id,
                TagName = _tagService.GetAll().FirstOrDefault(x => x.Id == postTag.Id).Title
            };

            return View(_postTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var postTag = _postTagService.Get(id);
            _postTagService.Delete(postTag);
            _postTagService.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string exeption)
        {
            return View((object)exeption);
        }
    }
}
