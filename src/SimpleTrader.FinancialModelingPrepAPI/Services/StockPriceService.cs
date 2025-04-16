using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Results;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        public async Task<double> GetPrice(string symbol)
        {
            using (FinancialModelingPrepAPI client = new FinancialModelingPrepAPI())
            {
                string url =
                    $"aftermarket-trade/?symbol=^{symbol}&apikey=LumwlleWnJLhYWnPIdB8Bf6pZZqd3sJO";

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
