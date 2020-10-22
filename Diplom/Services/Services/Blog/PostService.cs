using Diplom.Interfaces;
using Diplom.Models.DataModel.Blog;
using Diplom.Repositories.Blog;
using Diplom.Services.Interfaces;
using Diplom.Services.Models.Blog;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Services.Services.Blog
{
    public class PostService : IService<PostDTO>
    {
        protected IGenericRepository<Post> repo;

        public PostService(IConfiguration configuration)
        {
            repo = new PostRepository(configuration);
        }
        public void Add(PostDTO postViewModel)
        {
            Post newPost = new Post
            {
                Id = postViewModel.Id,
                Title = postViewModel.Title,
                Discription = postViewModel.Discription,
                Created = postViewModel.Created,
                Image = postViewModel.Image
            };
            repo.Add(newPost);
        }

        public void Delete(PostDTO entity)
        {
            Post delPost = repo.Get(entity.Id);
            repo.Delete(delPost);
        }

        public PostDTO Get(int id)
        {
            Post posts = repo.Get(id);
            PostDTO postsViewModel = new PostDTO();

            postsViewModel.Id = posts.Id;
            postsViewModel.Title = posts.Title;
            postsViewModel.Discription = posts.Discription;
            postsViewModel.Created = posts.Created;
            postsViewModel.Image = posts.Image;

            return postsViewModel;
        }

        public IEnumerable<PostDTO> GetAll()
        {
            return repo
                        .GetAll()
                          .Select(x => new PostDTO
                          {
                              Id = x.Id,
                              Title = x.Title,
                              Discription = x.Discription,
                              Created = x.Created,
                              Image = x.Image
                          });
        }

        public void Save()
        {
            repo.Save();
        }

        public void Update(PostDTO postsViewModel)
        {
            Post newPosts = repo.Get(postsViewModel.Id);
            if (newPosts != null)
            {
                newPosts.Id = postsViewModel.Id;
                newPosts.Title = postsViewModel.Title;
                newPosts.Discription = postsViewModel.Discription;
                newPosts.Created = postsViewModel.Created;
                newPosts.Image = postsViewModel.Image;
            }
            repo.Save();
        }
    }
}
