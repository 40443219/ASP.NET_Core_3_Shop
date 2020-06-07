using System;
using System.Threading.Tasks;

using Shop.Domain.Models;
using Shop.Domain.Infrastructure;

namespace Shop.Application.ProductsAdmin
{
    [Service]
    public class CreateProduct
    {
        private readonly IProductManager _productManager;

        public CreateProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<Response> Do(Request vm)
        {
            var newProduct = new Product
            {
                Name = vm.Name,
                Description = vm.Description,
                Value = vm.Value
            };

            var result = await _productManager.CreateProduct(newProduct) > 0;
            if(!result)
            {
                throw new Exception("Failed to create product!");
            }

            return new Response 
                {
                    Id = newProduct.Id,
                    Name = newProduct.Name,
                    Description = newProduct.Description,
                    Value = newProduct.Value
                };
        }

        public class Request
        {
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