using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diplom.Models.ViewModels;
using Diplom.Models.ViewModels.Blog;
using Diplom.Services.Interfaces;
using Diplom.Services.Models.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    public class BlogController : Controller
    {
        IService<PostDTO> _postService;
        IService<PostTagDTO> _postTagService;

        public int PageSize = 6;

        public BlogController(IService<PostDTO> postService,
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
    }
}
