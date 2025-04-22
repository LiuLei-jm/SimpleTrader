using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockIndexService : IStockIndexService
    {
        private readonly FinancialModelingPrepHttpClientFactory _httpClientFactory;

        public StockIndexService(FinancialModelingPrepHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<StockIndex?> GetStockIndex(string symbol)
        {
            using (FinancialModelingPrepHttpClient client = _httpClientFactory.CreateHttpClient())
            {
                string uri = $"quote-short/?symbol=^{symbol}";

                var stockIndexResult = await client.GetAsync<List<StockIndex>>(uri);

                var shortQuote = stockIndexResult.FirstOrDefault();
                return shortQuote;
            }
        }
    }
}
