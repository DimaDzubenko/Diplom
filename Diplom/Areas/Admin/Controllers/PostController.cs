using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diplom.Helpers;
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
    public class PostController : Controller
    {
        IService<PostDTO> _postService;
        IService<PostTagDTO> _postTagService;

        public int PageSize = 6;

        public PostController(IService<PostDTO> postService,
                                 IService<PostTagDTO> postTagService)
        {
            _postService = postService;
            _postTagService = postTagService;
        }

        public ActionResult Index(int page = 1)
        {
            var posts = _postService.GetAll();

            return View(new PostListViewModel
            {
                Posts = posts
                    .OrderBy(c => c.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PageViewModel = new PageViewModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = posts.Count()
                }
            });
        }

        public ActionResult Details(int id)
        {
            PostDTO post = null;
            try
            {
                post = _postService.Get(id);
            }
            catch
            {
                return RedirectToAction("Error", new { exeption = "The author doesn't exist in database anymore!" });
            }
            if (post == null)
            {
                return NotFound();
            }
            var _post = new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Discription = post.Discription,
                Created = post.Created
            };

            ViewBag.Image = post.Image;
            
            return View(_post);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel _post)
        {
            PostDTO post = new PostDTO();
            if (ModelState.IsValid)
            {
                if (_postService.GetAll().Where(x => x.Title == _post.Title).Count() > 0)
                {
                    return RedirectToAction("Error", new { exeption = "The author already exist!" });
                }
                else
                {
                    post.Id = _post.Id;
                    post.Title = _post.Title;
                    post.Discription = _post.Discription;
                    post.Created = _post.Created;                    
                    post.Image = _post.Image != null ? ImageConverter.GetBytes(_post.Image) : null;
                    _postService.Add(post);
                    _postService.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_post);
        }

        
        public ActionResult Edit(int id)
        {
            var post = _postService.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            var _post = new PostViewModel
            {                
                Title = post.Title,
                Discription = post.Discription,
                Created = post.Created
            };

            ViewBag.Image = post.Image;
            return View(_post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PostViewModel _post)
        {
            var post = _postService.Get(id);
            if (ModelState.IsValid)
            {
                try
                {
                    post.Id = _post.Id;
                    post.Title = _post.Title;
                    post.Discription = _post.Discription;
                    post.Created = _post.Created;
                    post.Image = _post.Image != null ? ImageConverter.GetBytes(_post.Image) : post.Image;
                    _postService.Update(post);
                    _postService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_post);
        }

        public ActionResult Delete(int id)
        {
            var post = _postService.Get(id);
            if (post == null)
            {
                return NotFound();
            }
            var _post = new PostViewModel
            {
                Title = post.Title,
                Discription = post.Discription,
                Created = post.Created
            };
            ViewBag.Image = post.Image;
            return View(_post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var post = _postService.Get(id);
            if (_postTagService.GetAll().Where(x => x.PostName == post.Title).Count() > 0)
            {
                return RedirectToAction("Error", new { exeption = "There are tattoos of the author you are trying to delete" });
            }
            else
            {
                _postService.Delete(post);
                _postService.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string exeption)
        {
            return View((object)exeption);
        }
    }
}
