using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Shop.Domain.Infrastructure;

namespace Shop.Application.Products
{
    [Service]
    public class GetProduct
    {
        private readonly IStockManager _stockManager;
        private readonly IProductManager _productManager;
        public GetProduct(IStockManager stockManager, IProductManager productManager)
        {
            _stockManager = stockManager;
            _productManager = productManager;
        }

        public async Task<ProductViewModel> Do(string name) {
            await _stockManager.RetrieveExpiredStockOnHold();
            
            return _productManager.GetProductByName(name,
                x => new ProductViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Value = x.Value.GetValueString(),
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
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public IEnumerable<StockViewModel> Stocks { get; set; }
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
        }
    }
}