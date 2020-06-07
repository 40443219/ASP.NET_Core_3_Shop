using System.Threading.Tasks;
using System;

using Shop.Domain.Models;
using Shop.Domain.Infrastructure;

namespace Shop.Application.StocksAdmin
{
    [Service]
    public class CreateStock
    {
        private readonly IStockManager _stockManager;
        public CreateStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task<Response> Do(Request vm)
        {
            var stock = new Stock
            {
                Description = vm.Description,
                Quantity = vm.Quantity,
                ProductId = vm.ProductId
            };

            var result = await _stockManager.CreateStock(stock) > 0; 
            if(!result)
            {
                throw new Exception("Fail to create stock!");
            }

           return new Response
           {
               Id = stock.Id,
               Description = stock.Description,
               Quantity = stock.Quantity
           }; 
        }

        public class Request
        {
            public string Description { get; set; }
            public int Quantity { get; set; }
            public int ProductId { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
        }
    }
}