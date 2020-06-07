using System.Threading.Tasks;
using System.Collections.Generic;

using Shop.Domain.Models;

namespace Shop.Domain.Infrastructure
{       
    public interface IStockManager
    {
        Stock GetStockWithProduct(int stockId);
        bool EnoughStock(int stockId, int quantity);
        Task PutStockOnHold(int stockId, int quantity, string sessionId);
        Task RetrieveExpiredStockOnHold();
        Task RemoveStockFromHold(string sessionId);
        Task RemoveStockFromHold(int stockId, int quantity, string sessionId);
        Task<int> CreateStock(Stock stock);
        Task<int> RemoveStock(int id);
        Task<int> UpdateStockRange(IEnumerable<Stock> stocks);
    }
}