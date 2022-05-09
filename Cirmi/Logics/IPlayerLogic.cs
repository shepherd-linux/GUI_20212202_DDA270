using Cirmi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Cirmi.Logics
{
    public interface IPlayerLogic
    {
        public Player Player { get; set; }
        public int SfxVolume { get; set; }
        public void Setup(Point location, List<ImageSource> sprites, IMapManagerLogic _mapLogic, IMenuManagerLogic _menuManagerLogic);
        public void Move(MovementDirection direction);
        public void OpenPauseMenu();
        public void Interact();
    }

    public enum MovementDirection
    {
        Down, Left, Right, Up
    }
}
