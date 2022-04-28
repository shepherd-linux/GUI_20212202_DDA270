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
    public class Floor : GameElement
    {
        public SimpleSprite Sprite { get; set; }

        public Floor(Point location, ImageSource image) : base(location, GameElementType.Floor)
        {
            if (image != null)
                Sprite = new SimpleSprite(image);
        }

    }
}
