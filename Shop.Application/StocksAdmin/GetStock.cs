using System.Collections.Generic;
using System.Linq;

using Shop.Domain.Infrastructure;

namespace Shop.Application.StocksAdmin
{
    [Service]
    public class GetStock
    {
        private readonly IProductManager _productManager;
        public GetStock(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Do()
        {
            return _productManager.GetProductWithStock(x => new ProductViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Stocks = x.Stocks.Select(y => new StockViewModel
                    {
                        Id = y.Id,
                        Description = y.Description,
                        Quantity = y.Quantity
                    })
                }
            );
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public IEnumerable<StockViewModel> Stocks;
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
        }
    }
}