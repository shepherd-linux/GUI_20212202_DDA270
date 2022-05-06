using Cirmi.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cirmi.Controller
{
    public class PlayerController : IPlayerController
    {
        IPlayerLogic playerLogic;

        public PlayerController(IPlayerLogic _playerLogic)
        {
            playerLogic = _playerLogic;
        }

        public void KeyPressed(Key key)
        {
            switch (key)
            {
                case Key.Up:
                    playerLogic.Move(MovementDirection.Up);
                    break;
                case Key.Down:
                    playerLogic.Move(MovementDirection.Down);
                    break;
                case Key.Left:
                    playerLogic.Move(MovementDirection.Left);
                    break;
                case Key.Right:
                    playerLogic.Move(MovementDirection.Right);
                    break;
                case Key.E:
                    playerLogic.Interact();
                    break;
                default:
                    break;
            }
        }
    }
}
