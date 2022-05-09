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
        bool canActivate;

        public ActionBlock(Point location, GameElementType elementType) : base(location, elementType)
        {
            ConnectedElements = new List<GameElement>();
            canActivate = false;
        }

        public void Activate()
        {
            if (ConnectedElements.Count == 1)
                canActivate = true;

            if (ConnectedElements.Count > 0)
            {
                foreach (var connElement in ConnectedElements)
                {
                    if ((connElement.ElementType == GameElementType.PushableBlock ||
                        (connElement.ElementType == GameElementType.CollectibleItem && (connElement as CollectableItem).ItemType == CollectableItemType.Placeable)) &&
                        connElement.Location == Location)
                        canActivate = true;
                    else if (canActivate)
                    {
                        switch (connElement.ElementType)
                        {
                            case GameElementType.Door:
                                (connElement as Door).Open();
                                break;
                            case GameElementType.ActionBlock:
                                (connElement as ActionBlock).Activate();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
