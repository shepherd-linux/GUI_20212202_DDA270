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
    /// Interaction logic for InventoryUserControl.xaml
    /// </summary>
    public partial class InventoryUserControl : UserControl
    {
        public InventoryUserControl() : this(Ioc.Default.GetService<IPlayerLogic>())
        {
        }

        public InventoryUserControl(IPlayerLogic _playerLogic)
        {
            InitializeComponent();
            var vm = new InventoryViewModel();
            this.DataContext = vm;
            vm.Setup(_playerLogic);
        }
    }
}
