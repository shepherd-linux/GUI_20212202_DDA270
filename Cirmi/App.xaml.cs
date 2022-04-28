using Cirmi.Controller;
using Cirmi.Logics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Cirmi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddSingleton<IMapManagerLogic, MapManagerLogic>()
                    .AddSingleton<IPlayerLogic, PlayerLogic>()
                    .AddSingleton<IPlayerController, PlayerController>()
                    .BuildServiceProvider()
                );
        }
    }
}
