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

namespace Cirmi.UserControls
{
    /// <summary>
    /// Interaction logic for GameOverUserControl.xaml
    /// </summary>
    public partial class GameOverUserControl : UserControl
    {
        public GameOverUserControl() : this(Ioc.Default.GetService<IPlayerLogic>(), Ioc.Default.GetService<IMapManagerLogic>())
        {
        }

        public GameOverUserControl(IPlayerLogic _playerLogic, IMapManagerLogic _mapLogic)
        {
            InitializeComponent();
            var vm = new GameOverViewModel();
            this.DataContext = vm;
            vm.Setup(_playerLogic, _mapLogic);
        }
    }
}
