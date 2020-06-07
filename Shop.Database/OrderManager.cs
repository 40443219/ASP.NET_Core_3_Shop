using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using Shop.Domain.Enums;

namespace Shop.Database
{
    public class OrderManager : IOrderManager
    {
        public readonly ApplicationDbContext _ctx;
        public OrderManager(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<int> CreateOrder(Order order)
        {
            _ctx.Orders.Add(order);

            return _ctx.SaveChangesAsync();
        }

        public IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus status, Func<Order, TResult> selector)
        {
            return _ctx.Orders
                .Where(x => x.Status == status)
                .Select(selector);
    }

        public TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector)
        {
            return GetOrder(x => x.Id == id, selector);
        }

        public TResult GetOrderByReference<TResult>(string reference, Func<Order, TResult> selector)
        {
            return GetOrder(x => x.OrderRef == reference, selector);
        }

        private TResult GetOrder<TResult>(Func<Order, bool> condition, Func<Order, TResult> selector)
        {
            return _ctx.Orders
                .Include(x => x.OrderStocks)
                .ThenInclude(x => x.Stock)
                .ThenInclude(x => x.Product)
                .AsEnumerable()
                .Where(x => condition(x))
                .Select(selector)
                .FirstOrDefault();
        }

        public Task<int> AdvenceOrder(int id)
        {
            var order = _ctx.Orders.FirstOrDefault(x => x.Id == id);

            order.Status += 1;

            return _ctx.SaveChangesAsync();
        }
    }
}