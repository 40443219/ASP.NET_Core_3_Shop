using System.Collections.Generic;

using Shop.Domain.Infrastructure;

namespace Shop.Application.ProductsAdmin
{
    [Service]
    public class GetProducts
    {
        private readonly IProductManager _productManager;
        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Do() => _productManager
            .GetProducts(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    // Description = x.Description,
                    Value = x.Value
                }
            );

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            // public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}