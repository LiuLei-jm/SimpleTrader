using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.State.Assets;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient(CreateHomeViewModel);
                services.AddTransient<PortfolioViewModel>();
                services.AddTransient<BuyViewModel>();
                services.AddTransient<SellViewModel>();
                services.AddTransient<AssetSummaryViewModel>();
                services.AddTransient<MainViewModel>();

                services.AddSingleton<CreateViewModel<HomeViewModel>>(services =>
                    () => services.GetRequiredService<HomeViewModel>()
                );
                services.AddSingleton<CreateViewModel<BuyViewModel>>(services =>
                    () => services.GetRequiredService<BuyViewModel>()
                );
                services.AddSingleton<CreateViewModel<SellViewModel>>(services =>
                    () => services.GetRequiredService<SellViewModel>()
                );
                services.AddSingleton<CreateViewModel<PortfolioViewModel>>(services =>
                    () => services.GetRequiredService<PortfolioViewModel>()
                );
                services.AddSingleton<CreateViewModel<RegisterViewModel>>(services =>
                    () => CreateRegisterViewModel(services)
                );
                services.AddSingleton<CreateViewModel<LoginViewModel>>(services =>
                    () => CreateLoginViewModel(services)
                );

                services.AddSingleton<
                    ISimpleTraderViewModelFactory,
                    SimpleTraderViewModelFactory
                >();
                services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();
            });
            return host;
        }


        private static HomeViewModel CreateHomeViewModel(IServiceProvider services)
        {
            return new HomeViewModel(
                StockIndexListingViewModel.LoadMajorIndexViewModel(
                    services.GetRequiredService<IStockIndexService>()
                ),
                services.GetRequiredService<AssetSummaryViewModel>()
            );
        }

        private static LoginViewModel CreateLoginViewModel(IServiceProvider services)
        {
            return new LoginViewModel(
                services.GetRequiredService<IAuthenticator>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>()
            );
        }

        private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
        {
            return new RegisterViewModel(
                services.GetRequiredService<IAuthenticator>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>()
            );
        }
    }
}
