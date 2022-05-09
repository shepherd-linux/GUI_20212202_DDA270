using Cirmi.Logics;
using Cirmi.SpriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Cirmi
{
    public class GameDisplay : FrameworkElement
    {
        IMapManagerLogic mapLogic;
        IPlayerLogic playerLogic;
        AnimationClock animClock;
        RectAnimation moveAnimation;

        public bool PlayerAnimationCompleted { get; set; }

        public void Setup(IMapManagerLogic _mapLogic, IPlayerLogic _playerLogic)
        {
            mapLogic = _mapLogic;
            playerLogic = _playerLogic;
            moveAnimation = new RectAnimation();
            moveAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(400));
            PlayerAnimationCompleted = true;
            mapLogic.LevelLoaded += InvalidateVisual;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (mapLogic != null && mapLogic.FloorElements != null)
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

            if (playerLogic != null && playerLogic.Player != null)
            {
                var newPos = new Rect(playerLogic.Player.Location.X, playerLogic.Player.Location.Y, 32, 32);
                moveAnimation.From = playerLogic.Player.Area!.Value;
                moveAnimation.To = newPos;
                PlayerAnimationCompleted = false;
                animClock = moveAnimation.CreateClock();
                animClock.Completed += (sender, e) => AnimationCompleted();
                playerLogic.Player.Area = newPos;
                drawingContext.DrawRectangle(playerLogic.Player.Sprite?.Brush, null, playerLogic.Player.Area!.Value, animClock);
            }
        }

        void AnimationCompleted()
        {
            playerLogic.Player.Sprite.PauseAnimation();
            PlayerAnimationCompleted = true;
        }
    }
}
