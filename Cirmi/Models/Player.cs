using Cirmi.SpriteModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cirmi.Models
{
    public class Player : GameElement
    {
        public CharacterSprite? Sprite { get; set; }
        public ObservableCollection<CollectableItem> Inventory { get; set; }
        public int Score { get { return Inventory.Where(c => c.Value > 0).Sum(c => c.Value); } }

        public Player(Point location) : base(location, GameElementType.Player)
        {
            Inventory = new ObservableCollection<CollectableItem>();
        }
    }
}
