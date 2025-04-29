using SimpleTrader.Domain.Models;

namespace SimpleTrader.Domain.Services
{
    public interface IStockIndexService
    {
        Task<StockIndex> GetStockIndex(string symbol);
    }
}
