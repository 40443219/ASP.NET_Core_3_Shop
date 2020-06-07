using Shop.Domain.Infrastructure;

namespace Shop.Application.ProductsAdmin
{
    [Service]
    public class GetProduct
    {
        private readonly IProductManager _productManger;
        public GetProduct(IProductManager productManger)
        {
            _productManger = productManger;
        }

        public ProductViewModel Do(int id) => _productManger
                .GetProductById(id, x => new ProductViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Value = x.Value
                    }
                );
        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}