using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Results;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly FinancialModelingPrepHttpClient _httpClient;

        public StockPriceService(FinancialModelingPrepHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<double> GetPrice(string symbol)
        {
            string url = $"aftermarket-trade/?symbol={symbol}";

            var stockPriceResult = await _httpClient.GetAsync<List<StockPriceResult>>(url);

            var firstResult = stockPriceResult?.FirstOrDefault();
            if (firstResult == null)
            {
                throw new InvalidSymbolException(symbol);
            }

            return firstResult.Price;
        }
    }
}
