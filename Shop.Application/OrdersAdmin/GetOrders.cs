using System.Collections.Generic;

using Shop.Domain.Infrastructure;
using Shop.Domain.Enums;

namespace Shop.Application.OrdersAdmin
{
    [Service]
    public class GetOrders
    {
        private readonly IOrderManager _orderManager;
        public GetOrders(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public IEnumerable<Response> Do(int status) =>
            _orderManager.GetOrdersByStatus((OrderStatus) status,
                x => new Response
                {
                    Id = x.Id,
                    OrderRef = x.OrderRef,
                    Email = x.Email
                }
            );

        public class Response
        {
            public int Id { get; set; }
            public string OrderRef { get; set; }
            public string Email { get; set; }
        }
    }
}