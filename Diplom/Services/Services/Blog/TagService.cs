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
    public class TagService : IService<TagDTO>
    {
        protected IGenericRepository<Tag> repo;

        public TagService(IConfiguration configuration)
        {
            repo = new TagRepository(configuration);
        }

        public void Add(TagDTO tagViewModel)
        {
            Tag newTag = new Tag
            {
                Id = tagViewModel.Id,
                Title = tagViewModel.Title
            };
            repo.Add(newTag);
        }

        public void Delete(TagDTO entity)
        {
            Tag delTag = repo.Get(entity.Id);
            repo.Delete(delTag);
        }

        public TagDTO Get(int id)
        {
            Tag tag = repo.Get(id);
            TagDTO tagsViewModel = new TagDTO();

            tagsViewModel.Id = tag.Id;
            tagsViewModel.Title = tag.Title;

            return tagsViewModel;
        }

        public IEnumerable<TagDTO> GetAll()
        {
            return repo
                        .GetAll()
                          .Select(x => new TagDTO
                          {
                              Id = x.Id,
                              Title = x.Title
                          });
        }

        public void Save()
        {
            repo.Save();
        }

        public void Update(TagDTO addressViewModel)
        {
            Tag newTag = repo.Get(addressViewModel.Id);
            if (newTag != null)
            {
                newTag.Id = addressViewModel.Id;
                newTag.Title = addressViewModel.Title;                
            }
            repo.Save();
        }
    }
}
