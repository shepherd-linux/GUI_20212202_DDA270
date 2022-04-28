using Cirmi.Controller;
using Cirmi.Models;
using Cirmi.Shared;
using Cirmi.SpriteModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Cirmi.Logics
{
    internal class MapManagerLogic : IMapManagerLogic
    {
        public List<List<GameElement>> MapElements { get; set; }
        public List<Floor> FloorElements { get; set; }

        public MapManagerLogic(IPlayerLogic playerLogic)
        {            
            MapElements = new List<List<GameElement>>();
            FloorElements = new List<Floor>();

            SetupPlayer(SetupLevel(), playerLogic);
        }

        Point SetupLevel()
        {
            var sprites = SpriteSheetManager.CutSheet(new BitmapImage(
                                        new Uri(Path.Combine(Directory.GetCurrentDirectory(), "sprites", "BaseChip_pipo.png"))));

            var lines = File.ReadAllLines("Levels/level_01.txt");
            var playerLoc = lines[0].Split(',');
            var floorFrame = sprites[int.Parse(lines[1].Split(':')[1])];
            var hasLayer2 = false;

            MapElements.Add(new List<GameElement>());

            int i = 2;
            for (; i < lines.Length; i++)
            {
                if(lines[i] == "--")
                {
                    hasLayer2 = true;
                    break;
                }

                var cols = lines[i].Split(',');
                for (int j = 0; j < cols.Length; j++)
                {

                    var loc = new Point( j * 32, (i - 2) * 32);
                    FloorElements.Add(new Floor(loc, floorFrame));

                    if (cols[j].Length > 1 && cols[j][0] == 'w')
                    {
                        var spl = cols[j].Split(':');
                        MapElements[0].Add(new GameElement(loc, GameElementType.Wall) { Sprite = new SimpleSprite(sprites[int.Parse(spl[1])]) });
                    }
                }
            }

            if(hasLayer2)
            {
                MapElements.Add(new List<GameElement>());
                var rowNum = 0;
                i++;

                for (; i < lines.Length; i++)
                {
                    var cols = lines[i].Split(',');
                    for (int j = 0; j < cols.Length; j++)
                    {
                        if (cols[j].Length > 1)
                        {
                            var loc = new Point ( j * 32, rowNum * 32);
                            var spl = cols[j].Split(':');

                            MapElements[1].Add(new GameElement(loc, (GameElementType)spl[0][0]) { Sprite = new SimpleSprite(sprites[int.Parse(spl[1])]) });
                        }
                    }
                    rowNum++;
                }
            }

            return new Point(int.Parse(playerLoc[0]) * 32, int.Parse(playerLoc[1]) * 32);
        }

        public void SetupPlayer(Point loctaion, IPlayerLogic playerLogic)
        {
            var sprites = SpriteSheetManager.CutSheet(
                                    new BitmapImage(
                                        new Uri(Path.Combine(Directory.GetCurrentDirectory(), "sprites", "pipo-nekonin008.png"))), 12);

            playerLogic.Setup(loctaion, sprites, this);
        }

        // Don't use IntersectWith because it's too accurate (tile edge issues)
        public bool IsColliding(Rect a)
        {
            return MapElements.Any(m => m.Any(e => !e.IsPermeable && a == e.Area));
        }
    }
}
