using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI
{
    public class FinancialModelingPrepAPI : HttpClient
    {
        public FinancialModelingPrepAPI()
        {
            this.BaseAddress = new Uri("https://financialmodelingprep.com/stable/");
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            HttpResponseMessage response = await GetAsync(BaseAddress + uri);
            string jsonResponse = await response.Content.ReadAsStringAsync();
            T? result = JsonConvert.DeserializeObject<T>(jsonResponse);

            if (result == null)
            {
                throw new InvalidOperationException("The deserialized object is null.");
            }

            return result;
        }
    }
}
