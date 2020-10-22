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
    public class PBrandService : IService<PBrandDTO>
    {
        protected IGenericRepository<PBrand> _repository;

        public PBrandService(IConfiguration configuration)
        {
            _repository = new PBrandRepository(configuration);
        }

        public void Add(PBrandDTO brandViewModel)
        {
            PBrand newBrand = new PBrand
            {
                PBrandId = brandViewModel.PBrandId,
                Name = brandViewModel.Name,
                Image = brandViewModel.Image
            };
            _repository.Add(newBrand);
        }

        public void Delete(PBrandDTO brandViewModel)
        {
            PBrand delBrand = _repository.Get(brandViewModel.PBrandId);
            _repository.Delete(delBrand);
        }

        public PBrandDTO Get(int id)
        {
            PBrand brand = _repository.Get(id);
            PBrandDTO brandViewModel = new PBrandDTO
            {
                PBrandId = brand.PBrandId,
                Name = brand.Name,
                Image = brand.Image
            };
            return brandViewModel;
        }

        public IEnumerable<PBrandDTO> GetAll()
        {
            return _repository
                        .GetAll()
                          .Select(x => new PBrandDTO
                          {
                              PBrandId = x.PBrandId,
                              Name = x.Name,
                              Image = x.Image
                          });
        }

        public void Save()
        {
            _repository.Save();
        }

        public void Update(PBrandDTO brandViewModel)
        {
            PBrand newBrand = _repository.Get(brandViewModel.PBrandId);
            if (newBrand != null)
            {
                newBrand.PBrandId = brandViewModel.PBrandId;
                newBrand.Name = brandViewModel.Name;
                newBrand.Image = brandViewModel.Image;
            }
            _repository.Save();
        }
    }
}
