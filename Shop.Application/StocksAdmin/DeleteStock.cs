using System;
using System.Threading.Tasks;

using Shop.Domain.Infrastructure;

namespace Shop.Application.StocksAdmin
{
    [Service]
    public class DeleteStock
    {
        private readonly IStockManager _stockManager;

        public DeleteStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task<bool> Do(int id)
        {
            var result = await _stockManager.RemoveStock(id) > 0;

            if(!result)
            {
                throw new Exception("Fail to remove stock!");
            }

            return true;
        }
    }
}