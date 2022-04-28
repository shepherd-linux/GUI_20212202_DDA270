using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cirmi.Controller
{
    public interface IPlayerController
    {
        //public void Move(MovementDirection direction);
        public void KeyPressed(Key key);
    }
}
