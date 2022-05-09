using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cirmi.Models
{
    public class PushableBlock : GameElement
    {
        public PushableBlock(Point location, GameElementType elementType) : base(location, elementType)
        {
        }

        public void Push(Point newLocation)
        {
            Location = newLocation;
            Area = new Rect(newLocation.X, newLocation.Y, 32, 32);
        }
    }
}
