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
    public class SelectLevelViewModel : ObservableRecipient
    {
        private Visibility visibility;
        private int selectedLevel;
        private bool level2Locked;
        private bool level3Locked;
        private bool level4Locked;
        private bool level5Locked;

        public Visibility Visibility { get => visibility; set => SetProperty(ref visibility, value); }

        public ICommand SelectLevel1 { get; set; }
        public ICommand SelectLevel2 { get; set; }
        public ICommand SelectLevel3 { get; set; }
        public ICommand SelectLevel4 { get; set; }
        public ICommand SelectLevel5 { get; set; }
        public ICommand StartGame { get; set; }
        public ICommand BackToMenu { get; set; }

        public bool Level2Locked { get => level2Locked; set
            {
                SetProperty(ref level2Locked, value);
                (SelectLevel2 as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public bool Level3Locked { get => level3Locked; set
            {
                SetProperty(ref level3Locked, value);
                (SelectLevel3 as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public bool Level4Locked { get => level4Locked; set
            {
                SetProperty(ref level4Locked, value);
                (SelectLevel4 as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public bool Level5Locked { get => level5Locked; set
            {
                SetProperty(ref level5Locked, value);
                (SelectLevel5 as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public int SelectedLevel { get => selectedLevel; set => SetProperty(ref selectedLevel, value); }
        public bool StartingGame { get; set; }

        public SelectLevelViewModel()
        {
            Visibility = Visibility.Collapsed;
            SelectedLevel = 1;
            StartingGame = false;
            level2Locked = level3Locked = level4Locked = level5Locked = false;
            SelectLevel1 = new RelayCommand(() => SelectedLevel = 1);
            SelectLevel2 = new RelayCommand(() => SelectedLevel = 2, () => !level2Locked);
            SelectLevel3 = new RelayCommand(() => SelectedLevel = 3, () => !level3Locked);
            SelectLevel4 = new RelayCommand(() => SelectedLevel = 4, () => !level4Locked);
            SelectLevel5 = new RelayCommand(() => SelectedLevel = 5, () => !level5Locked);
            StartGame = new RelayCommand(() => { StartingGame = true; Visibility = Visibility.Collapsed; });
            BackToMenu = new RelayCommand(() => { StartingGame = false; Visibility = Visibility.Collapsed; });
        }
    }
}
