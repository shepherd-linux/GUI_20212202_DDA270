using Cirmi.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cirmi.Models
{
    internal class PushableBlock : GameElement
    {
        public  Player player;
        Point location;
        IMapManagerLogic mapLogic;
        public PushableBlock(Point _location, GameElementType elementType, Player _player, IMapManagerLogic _mapLogic) : base(_location, elementType)
        {
            player = _player;
            location = _location;
            mapLogic = _mapLogic;
        }

        public void Push()
        {//wrong approach, consult
            Point newLocation = new Point();
            
            if (player.Location.X < location.X && player.Location.Y == location.Y) //push right
            {
                newLocation.X = location.X + 32;
                newLocation.Y = location.Y;
            }
            else if (player.Location.X > location.X && player.Location.Y == location.Y) //push left
            {
                newLocation.X = location.X - 32;
                newLocation.Y = location.Y;
            }
            else if (player.Location.Y < location.Y && player.Location.X == location.X) //push down
            {
                newLocation.X = location.X;
                newLocation.Y = location.Y + 32;
            }
            else if (player.Location.Y > location.Y && player.Location.X == location.X) //push up
            {
                newLocation.X = location.X;
                newLocation.Y = location.Y - 32;
            }

            var newRect = new Rect(newLocation.X, newLocation.Y, player.Area!.Value.Width, player.Area!.Value.Height);

            if (!mapLogic.IsColliding(newRect))
                location = newLocation;
        }
    }
}
