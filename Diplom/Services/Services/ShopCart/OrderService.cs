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
    public class OrderService : IService<OrderDTO>
    {
        protected IGenericRepository<Order> repo;

        public OrderService(IConfiguration configuration)
        {
            repo = new OrderRepository(configuration);
        }

        public void Add(OrderDTO orderViewModel)
        {
            Order newOrder = new Order
            {
                OrderId = orderViewModel.OrderId,
                CustomerId = orderViewModel.CustomerId,
                AddressId = orderViewModel.AddressId,
                CountryId = orderViewModel.CountryId,
                TotalValue = orderViewModel.TotalValue
            };
            repo.Add(newOrder);
            repo.Save();
        }

        public void Delete(OrderDTO entity)
        {
            Order delOrder = repo.Get(entity.OrderId);
            repo.Delete(delOrder);
        }

        public OrderDTO Get(int id)
        {
            Order order = repo.Get(id);
            OrderDTO orderViewModel = new OrderDTO();

            orderViewModel.OrderId = order.OrderId;
            orderViewModel.CustomerId = order.CustomerId;
            orderViewModel.CreatedOn = order.CreatedOn;
            orderViewModel.AddressId = order.AddressId;
            orderViewModel.CountryId = order.CountryId;
            orderViewModel.AddressLine = order.Address?.AddressLine;
            orderViewModel.TotalValue = order.TotalValue;

            return orderViewModel;
        }

        public IEnumerable<OrderDTO> GetAll()
        {
            return repo
                       .GetAll()
                         .Select(x => new OrderDTO
                         {
                             OrderId = x.OrderId,
                             CustomerId = x.CustomerId,
                             CreatedOn = x.CreatedOn,
                             AddressId = x.AddressId,
                             CountryId = x.CountryId,
                             AddressLine = x.Address.AddressLine,
                             TotalValue = x.TotalValue
                         });
        }

        public void Save()
        {
            repo.Save();
        }

        public void Update(OrderDTO orderViewModel)
        {
            Order newOrder = repo.Get(orderViewModel.OrderId);
            if (newOrder != null)
            {
                newOrder.OrderId = orderViewModel.OrderId;
                newOrder.CustomerId = orderViewModel.CustomerId;
                newOrder.AddressId = orderViewModel.AddressId;
                newOrder.CountryId = orderViewModel.CountryId;
                newOrder.TotalValue = orderViewModel.TotalValue;
            }
            repo.Save();
        }
    }
}
