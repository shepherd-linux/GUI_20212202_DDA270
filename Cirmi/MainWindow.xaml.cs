using Cirmi.Controller;
using Cirmi.Logics;
using Cirmi.ViewModels;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cirmi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IPlayerController playerController;

        public MainWindow() : this(Ioc.Default.GetService<IMenuManagerLogic>(), Ioc.Default.GetService<IMapManagerLogic>(), Ioc.Default.GetService<IPlayerLogic>(), Ioc.Default.GetService<IPlayerController>())
        {
        }

        public MainWindow(IMenuManagerLogic menuLogic, IMapManagerLogic mapLogic, IPlayerLogic playerLogic, IPlayerController _playerController)
        {
            InitializeComponent();
            menuLogic.Setup((mainMenuUC.DataContext as MainMenuViewModel), (settingsUC.DataContext as SettingsViewModel),
                (storeUC.DataContext as StoreViewModel), (selectLevelUC.DataContext as SelectLevelViewModel),
                (pauseMenuUC.DataContext as PauseMenuViewModel), (gameOverUC.DataContext as GameOverViewModel),
                (inventoryUC.DataContext as InventoryViewModel), mapLogic);
            playerController = _playerController;
            gameDisplay.Setup(mapLogic, playerLogic);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(gameDisplay.PlayerAnimationCompleted)
            {
                playerController.KeyPressed(e.Key);
                gameDisplay.InvalidateVisual();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gameDisplay.InvalidateVisual();
        }
    }
}
