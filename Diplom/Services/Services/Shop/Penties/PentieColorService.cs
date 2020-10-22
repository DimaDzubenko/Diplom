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
    public class PentieColorService : IService<PentieColorDTO>
    {
        protected IGenericRepository<PentieColor> repository;

        public PentieColorService(IConfiguration configuration)
        {
            repository = new PentieColorRepository(configuration);
        }

        public void Add(PentieColorDTO pentieColorViewModel)
        {
            PentieColor newPentieColor = new PentieColor
            {
                Id = pentieColorViewModel.Id,
                PentieId = pentieColorViewModel.PentieId,
                PColorId = pentieColorViewModel.ColorId
            };
            repository.Add(newPentieColor);
        }

        public void Delete(PentieColorDTO entity)
        {
            PentieColor delPentieColor = repository.Get(entity.Id);
            repository.Delete(delPentieColor);
        }

        public PentieColorDTO Get(int id)
        {
            PentieColor pentieColor = repository.Get(id);
            return new PentieColorDTO
            {
                Id = pentieColor.Id,
                PentieId = pentieColor.PentieId,
                PentieName = pentieColor.Pentie.Name,
                ColorId = pentieColor.PColorId,
                ColorName = pentieColor.PColor.Name
            };
        }

        public IEnumerable<PentieColorDTO> GetAll()
        {
            return repository
                        .GetAll()
                          .Select(pentieColor => new PentieColorDTO
                          {
                              Id = pentieColor.Id,
                              PentieId = pentieColor.PentieId,
                              PentieName = pentieColor.Pentie.Name,
                              ColorId = pentieColor.PColorId,
                              ColorName = pentieColor.PColor.Name
                          });
        }

        public void Save()
        {
            repository.Save();
        }

        public void Update(PentieColorDTO pentieColorViewModel)
        {
            PentieColor newPentieColor = repository.Get(pentieColorViewModel.Id);
            if (newPentieColor != null)
            {
                newPentieColor.Id = pentieColorViewModel.Id;
                newPentieColor.PentieId = pentieColorViewModel.PentieId;
                newPentieColor.PColorId = pentieColorViewModel.ColorId;
            }
            repository.Save();
        }
    }
}
