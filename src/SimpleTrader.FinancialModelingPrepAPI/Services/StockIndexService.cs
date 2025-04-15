using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockIndexService : IStockIndexService
    {
        public async Task<StockIndex> GetStockIndex(string symbol)
        {
            using (HttpClient client = new HttpClient())
            {
                string url =
                    $"https://financialmodelingprep.com/stable/quote-short/?symbol=^{symbol}&apikey=LumwlleWnJLhYWnPIdB8Bf6pZZqd3sJO";

                HttpResponseMessage response = await client.GetAsync(url);
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var indexShortQuote = JsonConvert.DeserializeObject<List<StockIndex>>(
                    jsonResponse
                );
                var shortQuote = indexShortQuote.FirstOrDefault();
                return shortQuote;
            }
            return null;
        }
    }
}
