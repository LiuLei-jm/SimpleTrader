using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Results;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly FinancialModelingPrepHttpClientFactory _httpClientFactory;

        public StockPriceService(FinancialModelingPrepHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<double> GetPrice(string symbol)
        {
            using (FinancialModelingPrepHttpClient client = _httpClientFactory.CreateHttpClient())
            {
                string url =
                    $"aftermarket-trade/?symbol={symbol}";

                var stockPriceResult = await client.GetAsync<List<StockPriceResult>>(url);

                var firstResult = stockPriceResult?.FirstOrDefault();
                if (firstResult == null)
                {
                    throw new InvalidSymbolException(symbol);
                }

                return firstResult.Price;
            }
        }
    }
}
