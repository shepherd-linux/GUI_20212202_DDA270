using Cirmi.Logics;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cirmi.ViewModels
{
    public class InventoryViewModel : ObservableRecipient
    {
        private string collectedItem;
        private int playerScore;

        public string CollectedItem { get => collectedItem; set => SetProperty(ref collectedItem, value); }
        public int PlayerScore { get => playerScore; set => SetProperty(ref playerScore, value); }
        public Visibility Visibility { get => visibility; set
            {
                SetProperty(ref visibility, value);

                if (visibility == Visibility.Visible)
                    OnVisible();
            }
        }

        IPlayerLogic playerLogic;
        private Visibility visibility;

        public InventoryViewModel()
        {
            Visibility = Visibility.Collapsed;
        }

        public void Setup(IPlayerLogic _playerLogic)
        {
            playerLogic = _playerLogic;
        }

        public void InventoryChanged()
        {
            CollectedItem = string.Join(", ", playerLogic.Player.Inventory.Where(i => i.Value == 0).Select(x => x.ItemType.ToString()));
            PlayerScore = playerLogic.Player.Score;
        }

        public void OnVisible()
        {
            if (playerLogic != null && playerLogic.Player != null)
                playerLogic.Player.Inventory.CollectionChanged += (sender, e) => InventoryChanged();
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
