using System.Configuration;
using System.Windows;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.EntityFramework;
using SimpleTrader.EntityFramework.Services;
using SimpleTrader.FinancialModelingPrepAPI;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Assets;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace SimpleTrader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(c =>
                {
                    c.AddJsonFile("appsettings.json");
                    c.AddEnvironmentVariables();
                })
                .ConfigureServices(
                    (context, services) =>
                    {
                        string? apiKey = context.Configuration.GetValue<string>("FINANCE_API_KEY");
                        if (string.IsNullOrEmpty(apiKey))
                        {
                            throw new ConfigurationErrorsException(
                                "The financeApiKey is not set in the app settings."
                            );
                        }
                        services.AddSingleton<FinancialModelingPrepHttpClientFactory>(
                            s => new FinancialModelingPrepHttpClientFactory(apiKey)
                        );
                        string? connectionString = context.Configuration.GetConnectionString(
                            "default"
                        );
                        if (string.IsNullOrEmpty(connectionString))
                        {
                            throw new ConfigurationErrorsException(
                                "The connection string is not set in the app settings."
                            );
                        }

                        services.AddDbContext<SimpleTraderDbContext>(options =>
                            options.UseSqlServer(connectionString)
                        );
                        services.AddSingleton<SimpleTraderDbContextFactory>(
                            new SimpleTraderDbContextFactory(connectionString)
                        );
                        services.AddSingleton<IAuthenticationService, AuthenticationService>();
                        services.AddSingleton<IDataService<Account>, AccountDataService>();
                        services.AddSingleton<IAccountService, AccountDataService>();
                        services.AddSingleton<IStockPriceService, StockPriceService>();
                        services.AddSingleton<IBuyStockService, BuyStockService>();
                        services.AddSingleton<IStockIndexService, StockIndexService>();

                        services.AddSingleton<IPasswordHasher, PasswordHasher>();

                        services.AddSingleton<
                            ISimpleTraderViewModelFactory,
                            SimpleTraderViewModelFactory
                        >();
                        services.AddSingleton<BuyViewModel>();
                        services.AddSingleton<PortfolioViewModel>();
                        services.AddSingleton<AssetSummaryViewModel>();
                        services.AddSingleton<HomeViewModel>(services => new HomeViewModel(
                            StockIndexListingViewModel.LoadMajorIndexViewModel(
                                services.GetRequiredService<IStockIndexService>()
                            ),
                            services.GetRequiredService<AssetSummaryViewModel>()
                        ));

                        services.AddSingleton<CreateViewModel<HomeViewModel>>(services =>
                        {
                            return () => services.GetRequiredService<HomeViewModel>();
                        });

                        services.AddSingleton<CreateViewModel<BuyViewModel>>(services =>
                        {
                            return () => services.GetRequiredService<BuyViewModel>();
                        });

                        services.AddSingleton<CreateViewModel<PortfolioViewModel>>(services =>
                        {
                            return () => services.GetRequiredService<PortfolioViewModel>();
                        });

                        services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
                        services.AddSingleton<CreateViewModel<RegisterViewModel>>(services =>
                        {
                            return () =>
                                new RegisterViewModel(
                                    services.GetRequiredService<IAuthenticator>(),
                                    services.GetRequiredService<
                                        ViewModelDelegateRenavigator<RegisterViewModel>
                                    >(),
                                    services.GetRequiredService<
                                        ViewModelDelegateRenavigator<LoginViewModel>
                                    >()
                                );
                        });

                        services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
                        services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();
                        services.AddSingleton<CreateViewModel<LoginViewModel>>(services =>
                        {
                            return () =>
                                new LoginViewModel(
                                    services.GetRequiredService<IAuthenticator>(),
                                    services.GetRequiredService<
                                        ViewModelDelegateRenavigator<HomeViewModel>
                                    >(),
                                    services.GetRequiredService<
                                        ViewModelDelegateRenavigator<RegisterViewModel>
                                    >()
                                );
                        });

                        services.AddSingleton<INavigator, Navigator>();
                        services.AddSingleton<IAuthenticator, Authenticator>();
                        services.AddSingleton<IAccountStore, AccountStore>();
                        services.AddSingleton<AssetStore>();
                        services.AddScoped<MainViewModel>();

                        services.AddScoped<MainWindow>(s => new MainWindow(
                            s.GetRequiredService<MainViewModel>()
                        ));
                    }
                );
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            Window window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
