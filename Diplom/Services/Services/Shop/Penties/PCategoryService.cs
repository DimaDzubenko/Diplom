using Diplom.Interfaces;
using Diplom.Models.DataModel.Shop.Penties;
using Diplom.Repositories.Shop.Penties;
using Diplom.Services.Interfaces;
using Diplom.Services.Models.Shop.Penties;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Services.Services.Shop.Penties
{
    public class PCategoryService : IService<PCategoryDTO>
    {
        protected IGenericRepository<PCategory> _repository;

        public PCategoryService(IConfiguration configuration)
        {
            _repository = new PCategoryRepository(configuration);
        }

        public void Add(PCategoryDTO categoryViewModel)
        {
            PCategory newCategory = new PCategory
            {
                PCategoryId = categoryViewModel.PCategoryId,
                Name = categoryViewModel.Name
            };
            _repository.Add(newCategory);
        }

        public void Delete(PCategoryDTO categoryViewModel)
        {
            PCategory delCategory = _repository.Get(categoryViewModel.PCategoryId);
            _repository.Delete(delCategory);
        }

        public PCategoryDTO Get(int id)
        {
            PCategory category = _repository.Get(id);
            PCategoryDTO categoryViewModel = new PCategoryDTO
            {
                PCategoryId = category.PCategoryId,
                Name = category.Name
            };
            return categoryViewModel;
        }

        public IEnumerable<PCategoryDTO> GetAll()
        {
            return _repository
                        .GetAll()
                          .Select(x => new PCategoryDTO
                          {
                              PCategoryId = x.PCategoryId,
                              Name = x.Name
                          });
        }

        public void Save()
        {
            _repository.Save();
        }

        public void Update(PCategoryDTO categoryViewModel)
        {
            PCategory newCategory = _repository.Get(categoryViewModel.PCategoryId);
            if (newCategory != null)
            {
                newCategory.PCategoryId = categoryViewModel.PCategoryId;
                newCategory.Name = categoryViewModel.Name;
            }
            _repository.Save();
        }
    }
}

