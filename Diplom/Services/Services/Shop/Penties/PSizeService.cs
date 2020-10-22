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
    public class PSizeService : IService<PSizeDTO>
    {
        protected IGenericRepository<PSize> _repository;

        public PSizeService(IConfiguration configuration)
        {
            _repository = new PSizeRepository(configuration);
        }

        public void Add(PSizeDTO sizeViewModel)
        {
            PSize newSize = new PSize
            {
                PSizeId = sizeViewModel.PSizeId,
                Name = sizeViewModel.Name
            };
            _repository.Add(newSize);
        }

        public void Delete(PSizeDTO sizeViewModel)
        {
            PSize delSize = _repository.Get(sizeViewModel.PSizeId);
            _repository.Delete(delSize);
        }

        public PSizeDTO Get(int id)
        {
            PSize size = _repository.Get(id);
            PSizeDTO sizeViewModel = new PSizeDTO
            {
                PSizeId = size.PSizeId,
                Name = size.Name
            };
            return sizeViewModel;
        }

        public IEnumerable<PSizeDTO> GetAll()
        {
            return _repository
                        .GetAll()
                          .Select(x => new PSizeDTO
                          {
                              PSizeId = x.PSizeId,
                              Name = x.Name
                          });
        }

        public void Save()
        {
            _repository.Save();
        }

        public void Update(PSizeDTO sizeViewModel)
        {
            PSize newSize = _repository.Get(sizeViewModel.PSizeId);
            if (newSize != null)
            {
                newSize.PSizeId = sizeViewModel.PSizeId;
                newSize.Name = sizeViewModel.Name;
            }
            _repository.Save();
        }
    }
}

