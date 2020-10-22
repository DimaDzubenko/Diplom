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
    public class PostTagService : IService<PostTagDTO>
    {
        protected IGenericRepository<PostTag> repo;

        public PostTagService(IConfiguration configuration)
        {
            repo = new PostTagRepository(configuration);
        }
        public void Add(PostTagDTO postTagViewModel)
        {
            PostTag newPostTags = new PostTag
            {
                Id = postTagViewModel.Id,
                PostId = postTagViewModel.PostId,
                TagId = postTagViewModel.TagId
            };
            repo.Add(newPostTags);
        }

        public void Delete(PostTagDTO entity)
        {
            PostTag delPostTags = repo.Get(entity.Id);
            repo.Delete(delPostTags);
        }

        public PostTagDTO Get(int id)
        {
            PostTag postTag = repo.Get(id);
            PostTagDTO postTagViewModel = new PostTagDTO();

            postTagViewModel.Id = postTag.Id;
            postTagViewModel.PostId = postTag.PostId;
            postTagViewModel.PostName = postTag.Post.Title;
            postTagViewModel.TagId = postTag.TagId;
            postTagViewModel.TagName = postTag.Tag.Title;
            

            return postTagViewModel;
        }

        public IEnumerable<PostTagDTO> GetAll()
        {
            return repo
                        .GetAll()
                          .Select(x => new PostTagDTO
                          {
                              Id = x.Id,
                              PostId = x.PostId,
                              PostName = x.Post.Title,
                              TagId = x.TagId,
                              TagName = x.Tag.Title,
                          });
        }

        public void Save()
        {
            repo.Save();
        }

        public void Update(PostTagDTO postTagViewModel)
        {
            PostTag newPostTag = repo.Get(postTagViewModel.Id);
            if (newPostTag != null)
            {
                newPostTag.Id = postTagViewModel.Id;
                newPostTag.PostId = postTagViewModel.PostId;                
                newPostTag.TagId = postTagViewModel.TagId;
                
            }
            repo.Save();
        }
    }
}
