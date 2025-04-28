using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.FinancialModelingPrepAPI;
using SimpleTrader.FinancialModelingPrepAPI.Models;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddFinanceAPIHostBuilderExtensions
    {
        public static IHostBuilder AddFinanceAPI(this IHostBuilder host)
        {
            host.ConfigureServices(
                (context, services) =>
                {
                    string? apiKey = context.Configuration.GetValue<string>("FINANCE_API_KEY");
                    if (string.IsNullOrEmpty(apiKey))
                    {
                        throw new ConfigurationErrorsException(
                            "The financeApiKey is not set in the app settings."
                        );
                    }
                    services.AddSingleton(new FinancialModelingPrepAPIKey(apiKey));
                    services.AddHttpClient<FinancialModelingPrepHttpClient>(c =>
                    {
                        c.BaseAddress = new Uri("https://financialmodelingprep.com/stable/");
                    });
                }
            );
            return host;
        }
    }
}
