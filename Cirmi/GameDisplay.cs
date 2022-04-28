using Cirmi.Logics;
using Cirmi.SpriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Cirmi
{
    public class GameDisplay : FrameworkElement
    {
        IMapManagerLogic mapLogic;
        IPlayerLogic playerLogic;

        public void Setup(IMapManagerLogic _mapLogic, IPlayerLogic _playerLogic)
        {
            mapLogic = _mapLogic;
            playerLogic = _playerLogic;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (mapLogic != null)
            {
                for (int i = 0; i < mapLogic.FloorElements.Count; i++)
                {
                    drawingContext.DrawRectangle(mapLogic.FloorElements[i].Sprite.Brush, null, mapLogic.FloorElements[i].Area!.Value);
                }

                for (int i = 0; i < mapLogic.MapElements.Count; i++)
                {
                    for (int j = 0; j < mapLogic.MapElements[i].Count; j++)
                    {
                        drawingContext.DrawRectangle(mapLogic.MapElements[i][j].Sprite?.Brush, null, mapLogic.MapElements[i][j].Area!.Value);
                    }
                }
            }

            if(playerLogic != null)
            {
                playerLogic.Player.Area = new Rect(playerLogic.Player.Location.X, playerLogic.Player.Location.Y, 32, 32);
                drawingContext.DrawRectangle(playerLogic.Player.Sprite?.Brush, null, playerLogic.Player.Area!.Value);
            }
        }
    }
}
