using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockIndexService : IStockIndexService
    {
        private readonly FinancialModelingPrepHttpClient _httpClient;

        public StockIndexService(FinancialModelingPrepHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<StockIndex?> GetStockIndex(string symbol)
        {
            string uri = $"quote-short/?symbol=^{symbol}";

            var stockIndexResult = await _httpClient.GetAsync<List<StockIndex>>(uri);

            var shortQuote = stockIndexResult.FirstOrDefault();
            return shortQuote;
        }
    }
}
