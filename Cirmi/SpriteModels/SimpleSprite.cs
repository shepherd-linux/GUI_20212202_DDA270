using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Cirmi.SpriteModels
{
    public class SimpleSprite : Sprite
	{
		public SimpleSprite(ImageSource image)
        {
			if(image != null)
				Brush = new ImageBrush(image);

			//spriteFrame = CutSheet(spriteSheet, row, col);
		}

		/*
        public int Row { get; }
        public int Col { get; }

        private ImageBrush CutSheet(BitmapImage spriteSheet, int row, int col)
		{
			Int32Rect cropRect = new Int32Rect(0, 0, frameWidth, frameHeight);
			BitmapImage sheet = spriteSheet;

			if ((sheet.PixelHeight % frameHeight) == 0 && (sheet.PixelWidth % frameWidth) == 0)
			{
				int currentY = row * frameHeight;
				int currentX = col * frameWidth;
				cropRect.X = currentX;
				cropRect.Y = currentY;
				return new ImageBrush(new CroppedBitmap(sheet, cropRect));
			}

			return new ImageBrush();
		}*/
	}
}
