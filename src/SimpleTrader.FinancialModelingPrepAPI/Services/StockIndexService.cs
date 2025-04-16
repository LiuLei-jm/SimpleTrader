using Newtonsoft.Json;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockIndexService : IStockIndexService
    {
        public async Task<StockIndex?> GetStockIndex(string symbol)
        {
            using (FinancialModelingPrepAPI client = new FinancialModelingPrepAPI())
            {
                string uri =
                    $"quote-short/?symbol=^{symbol}&apikey=LumwlleWnJLhYWnPIdB8Bf6pZZqd3sJO";

                var stockIndexResult = await client.GetAsync<List<StockIndex>>(uri);

                var shortQuote = stockIndexResult.FirstOrDefault();
                return shortQuote;
            }
        }
    }
}
