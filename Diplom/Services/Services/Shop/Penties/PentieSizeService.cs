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
    public class PentieSizeService : IService<PentieSizeDTO>
    {
        protected IGenericRepository<PentieSize> repository;

        public PentieSizeService(IConfiguration configuration)
        {
            repository = new PentieSizeRepository(configuration);
        }

        public void Add(PentieSizeDTO pentieSizeViewModel)
        {
            PentieSize newPentieSize = new PentieSize
            {
                Id = pentieSizeViewModel.Id,
                PentieId = pentieSizeViewModel.PentieId,
                PSizeId = pentieSizeViewModel.SizeId
            };
            repository.Add(newPentieSize);
        }

        public void Delete(PentieSizeDTO entity)
        {
            PentieSize delPentieSize = repository.Get(entity.Id);
            repository.Delete(delPentieSize);
        }

        public PentieSizeDTO Get(int id)
        {
            PentieSize pentieSize = repository.Get(id);
            return new PentieSizeDTO
            {
                Id = pentieSize.Id,
                PentieId = pentieSize.PentieId,
                PentieName = pentieSize.Pentie.Name,
                SizeId = pentieSize.PSizeId,
                SizeName = pentieSize.PSize.Name
            };
        }

        public IEnumerable<PentieSizeDTO> GetAll()
        {
            return repository
                        .GetAll()
                          .Select(pentieSize => new PentieSizeDTO
                          {
                              Id = pentieSize.Id,
                              PentieId = pentieSize.PentieId,
                              PentieName = pentieSize.Pentie.Name,
                              SizeId = pentieSize.PSizeId,
                              SizeName = pentieSize.PSize.Name
                          });
        }

        public void Save()
        {
            repository.Save();
        }

        public void Update(PentieSizeDTO pentieSizeViewModel)
        {
            PentieSize newPentieSize = repository.Get(pentieSizeViewModel.Id);
            if (newPentieSize != null)
            {
                newPentieSize.Id = pentieSizeViewModel.Id;
                newPentieSize.PentieId = pentieSizeViewModel.PentieId;
                newPentieSize.PSizeId = pentieSizeViewModel.SizeId;
            }
            repository.Save();
        }
    }
}
