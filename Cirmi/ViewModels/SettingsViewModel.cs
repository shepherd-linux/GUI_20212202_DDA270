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
    public class SettingsViewModel : ObservableRecipient
    {
        private int musicVolume;
        private int sFXVolume;
        private Visibility visibility;

        public int MusicVolume { get => musicVolume; set => SetProperty(ref musicVolume, value); }
        public int SFXVolume { get => sFXVolume; set => SetProperty(ref sFXVolume, value); }
        public Visibility Visibility { get => visibility; set => SetProperty(ref visibility, value); }

        public ICommand BackToMenu { get; set; }

        public SettingsViewModel()
        {
            Visibility = Visibility.Collapsed;
            BackToMenu = new RelayCommand(() => Visibility = Visibility.Collapsed);
        }
    }
}
