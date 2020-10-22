using Diplom.Interfaces;
using Diplom.Models.DataModel.ShopCart;
using Diplom.Repositories.ShopCart;
using Diplom.Services.Interfaces;
using Diplom.Services.Models.ShopCart;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Services.Services.ShopCart
{
    public class CountryService : IService<CountryDTO>
    {
        protected IGenericRepository<Country> repo;

        public CountryService(IConfiguration configuration)
        {
            repo = new CountryRepository(configuration);
        }

        public void Add(CountryDTO countryViewModel)
        {
            Country newCountry = new Country
            {
                CountryId = countryViewModel.CountryId,
                Name = countryViewModel.Name,
                ShortName = countryViewModel.ShortName
            };
            repo.Add(newCountry);
        }

        public void Delete(CountryDTO countryViewModel)
        {
            Country delCountry = repo.Get(countryViewModel.CountryId);
            repo.Delete(delCountry);
        }

        public CountryDTO Get(int id)
        {
            Country country = repo.Get(id);
            CountryDTO countryViewModel = new CountryDTO
            {
                CountryId = country.CountryId,
                Name = country.Name,
                ShortName = country.ShortName
            };
            return countryViewModel;
        }

        public IEnumerable<CountryDTO> GetAll()
        {
            return repo
                        .GetAll()
                          .Select(x => new CountryDTO
                          {
                              CountryId = x.CountryId,
                              Name = x.Name,
                              ShortName = x.ShortName
                          });
        }

        public void Save()
        {
            repo.Save();
        }

        public void Update(CountryDTO countryViewModel)
        {
            Country newCountry = repo.Get(countryViewModel.CountryId);
            if (newCountry != null)
            {
                newCountry.CountryId = countryViewModel.CountryId;
                newCountry.Name = countryViewModel.Name;
                newCountry.ShortName = countryViewModel.ShortName;
            }
            repo.Save();
        }
    }
}
