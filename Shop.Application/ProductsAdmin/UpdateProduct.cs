using System;
using System.Threading.Tasks;

using Shop.Domain.Infrastructure;

namespace Shop.Application.ProductsAdmin
{
    [Service]
    public class UpdateProduct
    {
        private readonly IProductManager _productManager;

        public UpdateProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<Response> Do(Request vm)
        {
            var product = _productManager.GetProductById(vm.Id, x => x);

            product.Name = vm.Name;
            product.Description = vm.Description;
            product.Value = vm.Value;

            var result = await _productManager.UpdateProduct(product) > 0;

            if(!result)
            {
                throw new Exception("Fail to remove product!");
            }

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            };
        }

        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}