using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Shop.Domain.Models;

namespace Shop.Domain.Infrastructure
{
    public interface IProductManager
    {
        TResult GetProductById<TResult>(int id, Func<Product, TResult> selector);
        TResult GetProductByName<TResult>(string name, Func<Product, TResult> selector);
        IEnumerable<TResult> GetProducts<TResult>(Func<Product, TResult> selector);
        IEnumerable<TResult> GetProductWithStock<TResult>(Func<Product, TResult> selector);
        Task<int> CreateProduct(Product product);
        Task<int> RemoveProductById(int id);
        Task<int> UpdateProduct(Product product);
    }
}