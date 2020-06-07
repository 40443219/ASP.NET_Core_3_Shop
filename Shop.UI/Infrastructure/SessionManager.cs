using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using Microsoft.AspNetCore.Http;

using Shop.Domain.Models;
using Shop.Domain.Infrastructure;

namespace Shop.UI.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void AddProduct(CartProduct cartProduct)
        {
            var stringObject = _session.GetString("cart");
            var cartList = new List<CartProduct>();

            if(!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonSerializer.Deserialize<List<CartProduct>>(stringObject);
            }

            if(cartList.Any(x => x.StockId == cartProduct.StockId))
            {
                cartList.Find(x => x.StockId == cartProduct.StockId).Quantity += cartProduct.Quantity;
            }
            else
            {
                cartList.Add(cartProduct);
            }

            stringObject = JsonSerializer.Serialize(cartList);

            _session.SetString("cart", stringObject);
        }

        public IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector)
        {
            var stringObject = _session.GetString("cart");

            if(string.IsNullOrEmpty(stringObject)) {
                return new List<TResult>();
            }
            
            var cartList = JsonSerializer.Deserialize<IEnumerable<CartProduct>>(stringObject);

            return cartList.Select(selector);
        }

        public string GetId() => _session.Id;

        public void AddCustomerInformation(CustomerInformation customer)
        {
            var stringObject = JsonSerializer.Serialize(customer);

            _session.SetString("customer-info", stringObject);
        }

        public void ClearCart()
        {
            _session.Remove("cart");
        }

        public CustomerInformation GetCustomerInformation()
        {
            var stringObject = _session.GetString("customer-info");

            if(string.IsNullOrEmpty(stringObject))
            {
                return null;
            }

            var customerInformation = JsonSerializer.Deserialize<CustomerInformation>(stringObject);

            return customerInformation;
        }

        public void RemoveProduct(int stockId, int quantity)
        {
            var stringObject = _session.GetString("cart");
            var cartList = new List<CartProduct>();

            if(string.IsNullOrEmpty(stringObject))
            {
                return;
            }

            cartList = JsonSerializer.Deserialize<List<CartProduct>>(stringObject);

            if(!cartList.Any(x => x.StockId == stockId))
            {
                return;
            }

            var cartStock = cartList.First(x => x.StockId == stockId);

            if(cartStock.Quantity > quantity)
            {
                cartStock.Quantity -= quantity;
            }
            else
            {
                cartStock.Quantity = 0;
                cartList.Remove(cartStock);
            }

            stringObject = JsonSerializer.Serialize(cartList);

            _session.SetString("cart", stringObject);
        }
    }
}