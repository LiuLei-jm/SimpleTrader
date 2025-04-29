using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.EntityFramework;
using SimpleTrader.FinancialModelingPrepAPI;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices(
                (context, services) =>
                {
                    string? connectionString = context.Configuration.GetConnectionString("sqlite");
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        throw new ConfigurationErrorsException(
                            "The connection string is not set in the app settings."
                        );
                    }
                    Action<DbContextOptionsBuilder> configureDbContext = o =>
                        o.UseSqlite(connectionString);
                    services.AddDbContext<SimpleTraderDbContext>(configureDbContext);
                    services.AddSingleton<SimpleTraderDbContextFactory>(
                        new SimpleTraderDbContextFactory(configureDbContext)
                    );
                }
            );
            return host;
        }
    }
}
