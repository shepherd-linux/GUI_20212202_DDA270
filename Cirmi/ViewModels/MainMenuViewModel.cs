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
    public class MainMenuViewModel : ObservableRecipient
    {
        private Visibility visibility;
        private int selectedMenu;

        public Visibility Visibility { get => visibility; set => SetProperty(ref visibility, value); }
        public int SelectedMenu { get => selectedMenu; set => SetProperty(ref selectedMenu, value); }

        public ICommand SelectLevel { get; set; }
        public ICommand Store { get; set; }
        public ICommand Settings { get; set; }
        public ICommand ExitGame { get; set; }

        public MainMenuViewModel()
        {
            SelectLevel = new RelayCommand(() => { Visibility = Visibility.Collapsed; SelectedMenu = 1; });
            Store = new RelayCommand(() => { Visibility = Visibility.Collapsed; SelectedMenu = 2; });
            Settings = new RelayCommand(() => { Visibility = Visibility.Collapsed; SelectedMenu = 3; });
            ExitGame = new RelayCommand(() => Environment.Exit(0));
        }
    }
}
