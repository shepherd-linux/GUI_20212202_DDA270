using Cirmi.Models;
using Cirmi.SpriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Cirmi.Logics
{
    internal class PlayerLogic : IPlayerLogic
    {
        public Player Player { get; set; }
        IMapManagerLogic mapLogic;

        public void Setup(Point location, List<ImageSource> sprites, IMapManagerLogic _mapLogic)
        {
            Player = new Player(location);
            Player.Sprite = new CharacterSprite(sprites);
            mapLogic = _mapLogic;
        }

        public void Move(MovementDirection direction)
        {
            Player.Sprite.StartAnimation();
            Player.Sprite.Direction = (int)direction;
            Point newLocation = new Point();

            switch (direction)
            {
                case MovementDirection.Up:
                    newLocation.X = Player.Location.X;
                    newLocation.Y = Player.Location.Y - 32;
                    
                    break;
                case MovementDirection.Right:
                    newLocation.X = Player.Location.X + 32;
                    newLocation.Y = Player.Location.Y;
                    break;
                case MovementDirection.Down:
                    newLocation.X = Player.Location.X;
                    newLocation.Y = Player.Location.Y + 32;
                    break;
                case MovementDirection.Left:
                    newLocation.X = Player.Location.X - 32;
                    newLocation.Y = Player.Location.Y;
                    break;
            }

            if(Player.Area != null)
            {
                var newRect = new Rect(newLocation.X, newLocation.Y, Player.Area!.Value.Width, Player.Area!.Value.Height);

                if (!mapLogic.IsColliding(newRect))
                    Player.Location = newLocation;
            }

            Player.Sprite.PauseAnimation();
        }
    }
}
