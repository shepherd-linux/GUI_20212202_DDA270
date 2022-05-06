using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cirmi.Models
{
    public class ActionBlock : GameElement
    {
        public List<GameElement> ConnectedElements { get; set; }

        public ActionBlock(Point location, GameElementType elementType) : base(location, elementType)
        {
            ConnectedElements = new List<GameElement>();
        }

        public void Activate()
        {
            if(ConnectedElements.Count > 0)
            {
                foreach (var connElement in ConnectedElements)
                {
                    switch (connElement.ElementType)
                    {
                        case GameElementType.Door:
                            (connElement as Door).Open();
                            break;
                        case GameElementType.PushableBlock:
                            break;
                        case GameElementType.ActionBlock:
                            (connElement as ActionBlock).Activate();
                            break;
                        case GameElementType.CollectibleItem:
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
