using Cirmi.SpriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Cirmi.Models
{
    public class GameElement
    {
        public Rect? Area { get; set; }
        public Point Location { get; set; }
        public GameElementType ElementType { get; set; }
        public bool IsPermeable { get; set; }
        public Sprite? Sprite { get; set; }

        public GameElement(Point location, GameElementType elementType)
        {
            Location = location;
            ElementType = elementType;

            IsPermeable = ElementType == GameElementType.Wall ||
                          ElementType == GameElementType.Door ||
                          ElementType == GameElementType.ImpermeableDecorationBlock ? false : true;

            Area = new Rect(location.X, location.Y, 32, 32);
        }
    }

    public enum GameElementType
    {
        Floor = 'f',
        Wall = 'w',
        Door = 'd',
        Player = 'p',
        PushableBlock = 'u',
        ActionBlock = 'a',
        DecorationBlock = 'e',
        ImpermeableDecorationBlock = 'i',
        CollectibleItem = 'c'
    }
}
