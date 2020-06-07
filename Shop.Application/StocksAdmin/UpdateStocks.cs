using System.Threading.Tasks;
using System.Collections.Generic;
using System;

using Shop.Domain.Models;
using Shop.Domain.Infrastructure;

namespace Shop.Application.StocksAdmin
{
    [Service]
    public class UpdateStocks
    {
        private readonly IStockManager _stockManager;
        public UpdateStocks(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task<Response> Do(Request vm)
        {
            var stocks = new List<Stock>();
            foreach (var stock in vm.Stocks)
            {
                stocks.Add(new Stock
                {
                    Id = stock.Id,
                    Description = stock.Description,
                    Quantity = stock.Quantity,
                    ProductId = stock.ProductId
                });
            }

            var result = await _stockManager.UpdateStockRange(stocks) > 0;
            if(!result)
            {
                throw new Exception("Fail to update stocks!");
            }
            
            return new Response
            {
                Stocks = vm.Stocks
            };
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public int ProductId { get; set; }
        }

        public class Request
        {
            public IEnumerable<StockViewModel> Stocks { get; set; }
        }

        public class Response
        {
            public IEnumerable<StockViewModel> Stocks { get; set; }
        }
    }
}