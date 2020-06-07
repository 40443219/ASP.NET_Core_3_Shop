using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

using Shop.Domain.Models;
using Shop.Domain.Infrastructure;

namespace Shop.Application.Orders
{
    [Service]
    public class CreateOrder
    {
        private readonly IOrderManager _orderManager;
        private readonly IStockManager _stockManager;
        public CreateOrder(IOrderManager orderManager, IStockManager stockManager)
        {
            _orderManager = orderManager;
            _stockManager = stockManager;
        }

        public async Task<bool> Do(Request request)
        {
            /*
            var stockIdList = request.Stocks.Select(x => x.StockId).ToList();

            var stocksToUpdate = _ctx.Stocks
                // .AsEnumerable()
                // .Where(x => request.Stocks.Any(y => y.StockId == x.Id))
                .Where(x => stockIdList.Contains(x.Id))
                .ToList();

            foreach (var stock in stocksToUpdate)
            {
                stock.Quantity = stock.Quantity - request.Stocks.FirstOrDefault(x => x.StockId == stock.Id).Quantity;
            }
            */

            var order = new Order
            {
                OrderRef = CreateOrderReference(),
                StripeReference = request.StripeReference,
                
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address1 = request.Address1,
                Address2 = request.Address2,
                City = request.City,
                PostCode = request.PostCode,
                OrderStocks = request.Stocks.Select(x => new OrderStock
                {
                    StockId = x.StockId,
                    Quantity = x.Quantity
                }).ToList()
            };

            var result = await _orderManager.CreateOrder(order) > 0;

            if(result)
            {
                await _stockManager.RemoveStockFromHold(request.SessionId);

                return true;
            }

            return false;
        }

        public string CreateOrderReference()
        {
            return Guid.NewGuid().ToString();
        }

        public class Stock
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }

        public class Request
        {
            public string StripeReference { get; set; }
            public string SessionId { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }

            public List<Stock> Stocks { get; set; }
        }
    }
}