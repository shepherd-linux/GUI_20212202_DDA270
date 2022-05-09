using Cirmi.Logics;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cirmi.ViewModels
{
    public class GameOverViewModel : ObservableRecipient
    {
        private int timeScore;
        private IPlayerLogic playerLogic;
        private IMapManagerLogic mapLogic;
        private Visibility visibility;

        public bool IsBackToMenuClicked { get; set; }

        public ICommand BackToMenu { get; set; }
        public ICommand LoadNextLevel { get; set; }

        public int TimeScore { get => timeScore; set => SetProperty(ref timeScore, value); }
        public Visibility Visibility { get => visibility; set {
                SetProperty(ref visibility, value);

                if(visibility == Visibility.Visible)
                    OnVisible();
            }
        }

        public GameOverViewModel()
        {
            if (!IsInDesignMode)
            {
                Visibility = Visibility.Collapsed;
                BackToMenu = new RelayCommand(() => {IsBackToMenuClicked = true; Visibility = Visibility.Collapsed; });
                LoadNextLevel = new RelayCommand(() => {IsBackToMenuClicked = false; Visibility = Visibility.Collapsed; });
            }
        }

        public void Setup(IPlayerLogic _playerLogic, IMapManagerLogic _mapLogic)
        {
            playerLogic = _playerLogic;
            mapLogic = _mapLogic;
        }

        public void OnVisible()
        {
            if(playerLogic != null && mapLogic.GameTimer.Elapsed.Seconds > 0)
                TimeScore = (int)(playerLogic.Player.Score + 100 / mapLogic.GameTimer.Elapsed.Seconds);
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
    }
}
