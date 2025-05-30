﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddViewsHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddScoped<MainWindow>(s => new MainWindow(
                    s.GetRequiredService<MainViewModel>()
                ));
            });
            return host;
        }
    }
}
