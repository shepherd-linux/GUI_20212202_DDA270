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
    public class StoreViewModel : ObservableRecipient
    {
        private int balance;
        private Visibility visibility;
        private int selectedSkin;
        private string skinPrice2;
        private string skinPrice3;
        private string skinPrice4;
        private string skinPrice5;
        private string skinPrice6;

        public int Balance { get => balance;
            set
            {
                SetProperty(ref balance, value);
                if(balance >= 20)
                {
                    (SelectSkin2 as RelayCommand).NotifyCanExecuteChanged();
                    (SelectSkin3 as RelayCommand).NotifyCanExecuteChanged();
                    (SelectSkin4 as RelayCommand).NotifyCanExecuteChanged();
                    (SelectSkin5 as RelayCommand).NotifyCanExecuteChanged();
                    (SelectSkin6 as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public Visibility Visibility { get => visibility; set => SetProperty(ref visibility, value); }
        public int SelectedSkin { get => selectedSkin; set => SetProperty(ref selectedSkin, value); }

        public string SkinPrice2 { get => skinPrice2; set => SetProperty(ref skinPrice2, value); }
        public string SkinPrice3 { get => skinPrice3; set => SetProperty(ref skinPrice3, value); }
        public string SkinPrice4 { get => skinPrice4; set => SetProperty(ref skinPrice4, value); }
        public string SkinPrice5 { get => skinPrice5; set => SetProperty(ref skinPrice5, value); }
        public string SkinPrice6 { get => skinPrice6; set => SetProperty(ref skinPrice6, value); }

        public ICommand SelectSkin1 { get; set; }
        public ICommand SelectSkin2 { get; set; }
        public ICommand SelectSkin3 { get; set; }
        public ICommand SelectSkin4 { get; set; }
        public ICommand SelectSkin5 { get; set; }
        public ICommand SelectSkin6 { get; set; }
        public ICommand BackToMenu { get; set; }

        public StoreViewModel()
        {
            Visibility = Visibility.Collapsed;
            SkinPrice2 = SkinPrice3 = SkinPrice4 = SkinPrice5 = SkinPrice6 = "20$";
            BackToMenu = new RelayCommand(() => Visibility = Visibility.Collapsed);
            SelectSkin1 = new RelayCommand(() => SelectedSkin = 3);
            SelectSkin2 = new RelayCommand(() => { if(SkinPrice2.Length > 2) Balance -= 20; SkinPrice2 = "0$"; SelectedSkin = 4; }, () => Balance >= 20 || SkinPrice2.Length == 2);
            SelectSkin3 = new RelayCommand(() => { if(SkinPrice3.Length > 2) Balance -= 20; SkinPrice3 = "0$"; SelectedSkin = 5; }, () => Balance >= 20 || SkinPrice3.Length == 2);
            SelectSkin4 = new RelayCommand(() => { if(SkinPrice4.Length > 2) Balance -= 20; SkinPrice4 = "0$"; SelectedSkin = 6; }, () => Balance >= 20 || SkinPrice4.Length == 2);
            SelectSkin5 = new RelayCommand(() => { if(SkinPrice5.Length > 2) Balance -= 20; SkinPrice5 = "0$"; SelectedSkin = 7; }, () => Balance >= 20 || SkinPrice5.Length == 2);
            SelectSkin6 = new RelayCommand(() => { if(SkinPrice6.Length > 2) Balance -= 20; SkinPrice6 = "0$"; SelectedSkin = 8; }, () => Balance >= 20 || SkinPrice6.Length == 2);
        }
    }
}
