using System.Threading.Tasks;

using Shop.Domain.Infrastructure;

namespace Shop.Application.OrdersAdmin
{
    public class UpdateOrder
    {
        [Service]
        private readonly IOrderManager _orderManager;

        public UpdateOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public Task<int> Do(int id)
        {
            return _orderManager.AdvenceOrder(id);
        }
    }
}