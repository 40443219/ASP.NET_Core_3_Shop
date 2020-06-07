using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Shop.Domain.Models;
using Shop.Domain.Enums;

namespace Shop.Domain.Infrastructure
{
    public interface IOrderManager
    {
        Task<int> CreateOrder(Order order);
        Task<int> AdvenceOrder(int id);
        IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus status, Func<Order, TResult> selector);
        TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector);
        TResult GetOrderByReference<TResult>(string reference, Func<Order, TResult> selector);
    }
}