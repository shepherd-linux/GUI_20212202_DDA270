using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cirmi.Models
{
    public class CollectableItem : GameElement
    {
        public CollectableItemType ItemType { get; set; }
        public int Value { get; set; }

        public CollectableItem(Point location, GameElementType elementType, CollectableItemType itemType = CollectableItemType.TunaCan, int value = 1) : base(location, elementType)
        {
            ItemType = itemType;
            Value = value;
        }
    }

    public enum CollectableItemType
    {
        TunaCan = 't',
        Key = 'k',
        Placeable = 'p'
    }
}
