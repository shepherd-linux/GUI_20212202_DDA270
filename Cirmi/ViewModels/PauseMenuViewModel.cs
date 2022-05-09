using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cirmi.ViewModels
{
    public class PauseMenuViewModel : ObservableRecipient
    {
        private Visibility visibility;
        private int selectedMenu;

        public Visibility Visibility { get => visibility; set => SetProperty(ref visibility, value); }
        public int SelectedMenu { get => selectedMenu; set => SetProperty(ref selectedMenu, value); }

        public ICommand Resume { get; set; }
        public ICommand Settings { get; set; }
        public ICommand ExitLevel { get; set; }

        public PauseMenuViewModel()
        {
            Visibility = Visibility.Collapsed;
            Resume = new RelayCommand(() => Visibility = Visibility.Collapsed);
            Settings = new RelayCommand(() => { Visibility = Visibility.Collapsed; SelectedMenu = 1; });
            ExitLevel = new RelayCommand(() => { Visibility = Visibility.Collapsed; SelectedMenu = 2; });
        }
    }
}
