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
    public class PentieService : IService<PentieDTO>
    {
        protected IGenericRepository<Pentie> _repository;

        public PentieService(IConfiguration configuration)
        {
            _repository = new PentieRepository(configuration);
        }

        public void Add(PentieDTO pentieViewModel)
        {
            Pentie newPentie = new Pentie
            {
                PentieId = pentieViewModel.PentieId,
                Name = pentieViewModel.Name,
                Discription = pentieViewModel.Discription,
                Price = pentieViewModel.Price,
                PBrandId = pentieViewModel.PBrandId,
                PCategoryId = pentieViewModel.PCategoryId,
                Image = pentieViewModel.Image
            };
            _repository.Add(newPentie);
        }

        public void Delete(PentieDTO entity)
        {
            Pentie delPentie = _repository.Get(entity.PentieId);
            _repository.Delete(delPentie);
        }

        public PentieDTO Get(int id)
        {
            Pentie pentie = _repository.Get(id);
            PentieDTO pentieViewModel = new PentieDTO();

            pentieViewModel.PentieId = pentie.PentieId;
            pentieViewModel.Name = pentie.Name;
            pentieViewModel.Discription = pentie.Discription;
            pentieViewModel.Price = pentie.Price;
            pentieViewModel.PBrandId = pentie.PBrandId;
            pentieViewModel.PBrandName = pentie.PBrand?.Name;
            pentieViewModel.PCategoryId = pentie.PCategoryId;
            pentieViewModel.PCategoryName = pentie.PCategory?.Name;
            pentieViewModel.Image = pentie.Image;

            return pentieViewModel;
        }

        public IEnumerable<PentieDTO> GetAll()
        {
            return _repository
                        .GetAll()
                          .Select(x => new PentieDTO
                          {
                              PentieId = x.PentieId,
                              Name = x.Name,
                              Discription = x.Discription,
                              Price = x.Price,
                              PBrandId = x.PBrandId,
                              PBrandName = x.PBrand.Name,
                              PCategoryId = x.PCategoryId,
                              PCategoryName = x.PCategory.Name,
                              Image = x.Image
                          });
        }

        public void Save()
        {
            _repository.Save();
        }

        public void Update(PentieDTO pentieViewModel)
        {
            Pentie newPentie = _repository.Get(pentieViewModel.PentieId);
            if (newPentie != null)
            {
                newPentie.PentieId = pentieViewModel.PentieId;
                newPentie.Name = pentieViewModel.Name;
                newPentie.Discription = pentieViewModel.Discription;
                newPentie.Price = pentieViewModel.Price;
                newPentie.PCategoryId = pentieViewModel.PCategoryId;
                newPentie.PBrandId = pentieViewModel.PBrandId;
                newPentie.Image = pentieViewModel.Image;
            }
            _repository.Save();
        }
    }
}
