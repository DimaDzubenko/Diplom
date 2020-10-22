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
    public class PColorService : IService<PColorDTO>
    {
        protected IGenericRepository<PColor> _repository;

        public PColorService(IConfiguration configuration)
        {
            _repository = new PColorRepository(configuration);
        }

        public void Add(PColorDTO colorViewModel)
        {
            PColor newColor = new PColor
            {
                PColorId = colorViewModel.PColorId,
                Name = colorViewModel.Name
            };
            _repository.Add(newColor);
        }

        public void Delete(PColorDTO colorViewModel)
        {
            PColor delColor = _repository.Get(colorViewModel.PColorId);
            _repository.Delete(delColor);
        }

        public PColorDTO Get(int id)
        {
            PColor color = _repository.Get(id);
            PColorDTO colorViewModel = new PColorDTO
            {
                PColorId = color.PColorId,
                Name = color.Name
            };
            return colorViewModel;
        }

        public IEnumerable<PColorDTO> GetAll()
        {
            return _repository
                        .GetAll()
                          .Select(x => new PColorDTO
                          {
                              PColorId = x.PColorId,
                              Name = x.Name
                          });
        }

        public void Save()
        {
            _repository.Save();
        }

        public void Update(PColorDTO colorViewModel)
        {
            PColor newColor = _repository.Get(colorViewModel.PColorId);
            if (newColor != null)
            {
                newColor.PColorId = colorViewModel.PColorId;
                newColor.Name = colorViewModel.Name;
            }
            _repository.Save();
        }
    }
}
