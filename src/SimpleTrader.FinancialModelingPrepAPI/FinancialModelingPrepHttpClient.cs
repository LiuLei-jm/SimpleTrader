using Newtonsoft.Json;
using SimpleTrader.FinancialModelingPrepAPI.Models;

namespace SimpleTrader.FinancialModelingPrepAPI
{
    public class FinancialModelingPrepHttpClient
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        public FinancialModelingPrepHttpClient(
            HttpClient client,
            FinancialModelingPrepAPIKey apiKey
        )
        {
            _client = client;
            _apiKey = apiKey.Key;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            HttpResponseMessage response = await _client.GetAsync($"{uri}&apikey={_apiKey}");
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
