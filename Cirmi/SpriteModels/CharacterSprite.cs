using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Cirmi.SpriteModels
{
    public class CharacterSprite : Sprite
    {
		private readonly List<ImageSource> animations;
		int idleFrame;

        public DispatcherTimer animTimer;
		bool animTimerIsRunning = false;
		int frameDuration = 100;
		int animIndex = 0;
		int direction = 0;

		public CharacterSprite(List<ImageSource> animations, int idleFrame = 1)
        {
			this.animations = animations;
			this.idleFrame = idleFrame;
			Brush = new ImageBrush(animations[idleFrame]);
			animTimer = new DispatcherTimer();
			animTimer.Tick += (sender, e) => NextFrame();
		}

		public int Direction { get { return direction; } set { direction = value; } }

		private void SetCurrentFrame(ImageSource source) { Brush.ImageSource = source; }

		/// <summary>
		/// Start sprite animation.
		/// </summary>
		public void StartAnimation()
		{
			if(!animTimerIsRunning)
            {
				animTimerIsRunning = true;
				animTimer.Interval = TimeSpan.FromMilliseconds(frameDuration);
				animTimer.Start();
			}
		}

		/// <summary>
		/// Pause running sprite animation
		/// </summary>
		public void PauseAnimation()
		{
			if (animTimerIsRunning)
			{
				SetCurrentFrame(animations[1 + (direction * 3)]);
				animTimer.Stop();
				animTimerIsRunning = false;
			}
		}

		/// <summary>
		/// Increase animation index by one or reset to zero if at end of list
		/// </summary>
		private void NextFrame()
		{
			animIndex++;
			animIndex %= 3;
			SetCurrentFrame(animations[animIndex+(direction*3)]);
		}
	}
}
