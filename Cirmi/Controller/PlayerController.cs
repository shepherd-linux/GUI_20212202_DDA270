using Cirmi.Logics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (playerLogic.Player != null && !playerLogic.Player.Sprite!.AnimTimerIsRunning)
            {
                switch (key)
                {
                    case Key.Up:
                    case Key.W:
                        playerLogic.Move(MovementDirection.Up);
                        break;
                    case Key.Down:
                    case Key.S:
                        playerLogic.Move(MovementDirection.Down);
                        break;
                    case Key.Left:
                    case Key.A:
                        playerLogic.Move(MovementDirection.Left);
                        break;
                    case Key.Right:
                    case Key.D:
                        playerLogic.Move(MovementDirection.Right);
                        break;
                    case Key.E:
                        playerLogic.Interact();
                        break;
                    case Key.Escape:
                        playerLogic.OpenPauseMenu();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
