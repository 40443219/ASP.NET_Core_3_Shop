using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

using Shop.Domain.Models;
using Shop.Domain.Infrastructure;

namespace Shop.Database
{
    public class StockManager : IStockManager
    {
        private readonly ApplicationDbContext _ctx;
        public StockManager(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool EnoughStock(int stockId, int quantity)
        {
            return _ctx.Stocks.FirstOrDefault(x => x.Id == stockId).Quantity >= quantity;
        }

        public Stock GetStockWithProduct(int stockId)
        {
            return _ctx.Stocks
                .Include(x => x.Product)
                .FirstOrDefault(x => x.Id == stockId);
        }

        // Database responsibility
        public Task PutStockOnHold(int stockId, int quantity, string sessionId)
        {
            // Begin transaction
            var stockToHold = _ctx.Stocks.FirstOrDefault(x => x.Id == stockId);
            stockToHold.Quantity -= quantity;

            var stockOnHold = _ctx.StockOnHold
                .Where(x => x.SessionId == sessionId)
                .ToList();
            if(stockOnHold.Any(x => x.StockId == stockId))
            {
                stockOnHold.Find(x => x.StockId == stockId).Quantity += quantity;
            }
            else
            {
                _ctx.StockOnHold.Add(new StockOnHold
                {
                    StockId = stockId,
                    Quantity = quantity,
                    ExpireDate = DateTime.Now.AddMinutes(20),
                    SessionId = sessionId
                });
            }

            foreach (var stock in stockOnHold)
            {
                stock.ExpireDate = DateTime.Now.AddMinutes(20);
            }

            return _ctx.SaveChangesAsync();
        }

        public Task RemoveStockFromHold(string sessionId)
        {
            var stocksOnHold = _ctx.StockOnHold
                .Where(x => x.SessionId == sessionId)
                .ToList();

            _ctx.StockOnHold.RemoveRange(stocksOnHold);

            return _ctx.SaveChangesAsync();
        }

        public Task RemoveStockFromHold(int stockId, int quantity, string sessionId)
        {
            var stockOnHold = _ctx.StockOnHold
                .FirstOrDefault(x => x.StockId == stockId && x.SessionId == sessionId);
            
            var stock = _ctx.Stocks.FirstOrDefault(x => x.Id == stockId);

            if(stockOnHold.Quantity < quantity)
            {
                stock.Quantity += stockOnHold.Quantity;
                stockOnHold.Quantity = 0;
            }
            else 
            {
                stock.Quantity += quantity;
                stockOnHold.Quantity -= quantity;
            }

            if(stockOnHold.Quantity <= 0)
            {
                _ctx.Remove(stockOnHold);
            }

            return _ctx.SaveChangesAsync();
        }

        public Task RetrieveExpiredStockOnHold()
        {
            var now = DateTime.Now;
            var stocksOnHold = _ctx.StockOnHold.Where(x => x.ExpireDate < now).ToList();

            if(stocksOnHold.Count > 0)
            {
                var stockIdList = stocksOnHold.Select(x => x.StockId).ToList();
                var stocksToReturn = _ctx.Stocks.Where(x => stockIdList.Contains(x.Id)).ToList();

                foreach (var stock in stocksToReturn)
                {
                    stock.Quantity += stocksOnHold.FirstOrDefault(x => x.StockId == stock.Id).Quantity;
                }

                _ctx.StockOnHold.RemoveRange(stocksOnHold);

                return _ctx.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }

        public Task<int> CreateStock(Stock stock)
        {
            _ctx.Stocks.Add(stock);

            return _ctx.SaveChangesAsync();
        }

        public Task<int> RemoveStock(int id)
        {
            var stock = _ctx.Stocks.FirstOrDefault(x => x.Id == id);
            _ctx.Stocks.Remove(stock);
            
            return _ctx.SaveChangesAsync();
        }

        public Task<int> UpdateStockRange(IEnumerable<Stock> stocks)
        {
            _ctx.Stocks.UpdateRange(stocks);

            return _ctx.SaveChangesAsync();
        }
        
    }
}