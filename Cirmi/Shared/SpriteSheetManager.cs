using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Cirmi.Shared
{
    static class SpriteSheetManager
    {
		public static List<ImageSource> CutSheet(BitmapImage spriteSheet, int numFrames = 0, int frameWidth = 32, int frameHeight = 32)
		{
			List<ImageSource> frames = new List<ImageSource>();

			Int32Rect cropRect = new Int32Rect(0, 0, frameWidth, frameHeight);
			BitmapImage sheet = spriteSheet;
			ImageSource frame;

			if (numFrames == 0)
				numFrames = (spriteSheet.PixelWidth / frameWidth) * (spriteSheet.PixelHeight / frameHeight);

			if ((sheet.PixelHeight % frameHeight) == 0 && (sheet.PixelWidth % frameWidth) == 0)
			{
				int rows = sheet.PixelHeight / frameHeight;
				int columns = sheet.PixelWidth / frameWidth;
				int frameCount = 0;
				for (int row = 0; row < rows; row++)
				{
					if (frameCount < numFrames)
					{
						for (int col = 0; col < columns; col++)
						{
							int currentY = row * frameHeight;
							int currentX = col * frameWidth;
							cropRect.X = currentX;
							cropRect.Y = currentY;

							frame = new CroppedBitmap(sheet, cropRect);
							frames.Add(frame);

							frameCount++;

							if (frameCount == numFrames) { break; }
						}
					}
					else { break; }
				}
			}
			return frames;
		}
	}
}
