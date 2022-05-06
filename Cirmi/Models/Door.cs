using Cirmi.SpriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cirmi.Models
{
    public class Door : GameElement
    {
        public SimpleSprite? ActiveStateSprite { get; set; }
        public SimpleSprite? DeactiveStateSprite { get; set; }
        public bool IsExitDoor { get; set; }

        public Door(Point location, GameElementType elementType, bool isExitDoor = false) : base(location, elementType)
        {
            IsExitDoor = isExitDoor;
        }

        public void Open()
        {
            Sprite = ActiveStateSprite;
            IsPermeable = true;
        }
    }
}
