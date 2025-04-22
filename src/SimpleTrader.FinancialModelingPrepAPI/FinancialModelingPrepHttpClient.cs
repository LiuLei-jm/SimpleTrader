using Newtonsoft.Json;

namespace SimpleTrader.FinancialModelingPrepAPI
{
    public class FinancialModelingPrepHttpClient : HttpClient
    {
        private readonly string _apiKey;
        public FinancialModelingPrepHttpClient(string apiKey)
        {
            this.BaseAddress = new Uri("https://financialmodelingprep.com/stable/");
            _apiKey = apiKey;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            var apikey = "LumwlleWnJLhYWnPIdB8Bf6pZZqd3sJO";
            HttpResponseMessage response = await GetAsync($"{uri}&apikey={_apiKey}");
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
