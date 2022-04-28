using Cirmi.SpriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cirmi.Models
{
    public class Player : GameElement
    {
        public CharacterSprite? Sprite { get; set; }

        public Player(Point location) : base(location, GameElementType.Player)
        {
        }
    }
}
