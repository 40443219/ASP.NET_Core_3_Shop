using System.Collections.Generic;
using System.Linq;

using Shop.Domain.Infrastructure;

namespace Shop.Application.Products
{
    [Service]
    public class GetProducts
    {
        private readonly IProductManager _productManager;
        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Do() => 
            _productManager.GetProducts(x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Value = x.Value.GetValueString(),
 
                StockCount = x.Stocks.Sum(y => y.Quantity)
            });
        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public int StockCount { get; set; }
        }
    }
}