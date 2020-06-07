using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Shop.Domain.Infrastructure;
using Shop.Domain.Models;

namespace Shop.Database
{
    public class ProductManager : IProductManager
    {
        private readonly ApplicationDbContext _ctx;
        public ProductManager(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<int> CreateProduct(Product product)
        {
            _ctx.Products.Add(product);
            return _ctx.SaveChangesAsync();
        }

        private TResult GetProduct<TResult>(Func <Product, bool> condition, Func<Product, TResult> selector)
        {
            return _ctx.Products
                .Include(x => x.Stocks)
                .AsEnumerable()
                .Where(x => condition(x))
                .Select(selector)
                .FirstOrDefault();
        }

        public TResult GetProductById<TResult>(int id, Func<Product, TResult> selector)
        {
            return GetProduct(x => x.Id == id, selector);
        }

        public TResult GetProductByName<TResult>(string name, Func<Product, TResult> selector)
        {
            return GetProduct(x => x.Name == name, selector);
        }

        public IEnumerable<TResult> GetProducts<TResult>(Func<Product, TResult> selector)
        {
            return _ctx.Products
                .Include(x => x.Stocks)
                .Select(selector)
                .ToList();
        }

        public Task<int> RemoveProductById(int id)
        {
            var Product = _ctx.Products.FirstOrDefault(x => x.Id == id);
            _ctx.Remove(Product);
            return _ctx.SaveChangesAsync();
        }

        public Task<int> UpdateProduct(Product product)
        {
            _ctx.Products.Update(product);

            return _ctx.SaveChangesAsync();
        }

        public IEnumerable<TResult> GetProductWithStock<TResult>(Func<Product, TResult> selector)
        {
            return _ctx.Products
                .Include(x => x.Stocks)
                .Select(selector);
        }
    }
}