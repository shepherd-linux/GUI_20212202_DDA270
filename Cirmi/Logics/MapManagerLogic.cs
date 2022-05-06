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

        Door[] exitDoor;
        int numberOfTunaCansRequired = 0;

        public MapManagerLogic(IPlayerLogic playerLogic)
        {            
            MapElements = new List<List<GameElement>> { new List<GameElement>() };
            FloorElements = new List<Floor>();
            exitDoor = new Door[2];

            SetupPlayer(SetupLevel(), playerLogic);
        }

        Point SetupLevel()
        {
            var sprites = SpriteSheetManager.CutSheet(new BitmapImage(
                                        new Uri(Path.Combine(Directory.GetCurrentDirectory(), "sprites", "BaseChip_pipo.png"))));

            var lines = File.ReadAllLines("Levels/level_01.txt");
            var firstLine = lines[0].Split(';');
            var playerLoc = ParsePoint(firstLine[0]);
            var exitLocation = ParsePoint(firstLine[1]);

            if(firstLine.Length > 2)
                numberOfTunaCansRequired = int.Parse(firstLine[2]);

            var floorFrame = sprites[int.Parse(lines[1].Split(':')[1])];
            var hasLayer2 = false;

            int i = 2;
            for (; i < lines.Length; i++)
            {
                if(lines[i].StartsWith("--"))
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
                        AddElementToMapElements(loc, sprites[int.Parse(spl[1])]);
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
                    if (lines[i].StartsWith("--"))
                    {
                        while (++i < lines.Length)
                        {
                            UpdateExistingMapElement(lines[i], sprites);
                        }

                        break;
                    }

                    var cols = lines[i].Split(',');

                    for (int j = 0; j < cols.Length; j++)
                    {
                        if (cols[j].Length > 1)
                        {
                            var loc = new Point ( j * 32, rowNum * 32);
                            var spl = cols[j].Split(':');
                            var isExitDoor = (loc == exitLocation || loc == new Point(exitLocation.X, exitLocation.Y - 32));
                            AddElementToMapElements(loc, sprites[int.Parse(spl[1])], (GameElementType)spl[0][0], 1, isExitDoor);
                        }
                    }
                    rowNum++;
                }
            }

            return playerLoc;
        }

        public void SetupPlayer(Point loctaion, IPlayerLogic playerLogic)
        {
            var sprites = SpriteSheetManager.CutSheet(
                                    new BitmapImage(
                                        new Uri(Path.Combine(Directory.GetCurrentDirectory(), "sprites", "pipo-nekonin008.png"))), 12);

            playerLogic.Setup(loctaion, sprites, this);
        }

        void UpdateExistingMapElement(string datas, List<ImageSource> sprites)
        {
            var pairs = datas.Split(';');
            var typeAndSprite = pairs[0].Split(':');
            var loc = ParsePoint(pairs[1]);
            var element = GetElementByLocation(loc, 1);

            switch ((GameElementType)typeAndSprite[0][0])
            {
                case GameElementType.Door:
                    var sprite = sprites[int.Parse(typeAndSprite[1])];
                    (element as Door).ActiveStateSprite = new SimpleSprite(sprite);
                    break;
                case GameElementType.ActionBlock:
                    var connElements = pairs.Skip(2).Select(p => GetElementByLocation(ParsePoint(p), 1));
                    (element as ActionBlock).ConnectedElements.AddRange(connElements);
                    break;
                case GameElementType.CollectibleItem:
                    break;
                default:
                    break;
            }
        }

        void AddElementToMapElements(Point location, ImageSource image = null, GameElementType elementType = GameElementType.Wall, int layer = 0, bool isExitDoor = false)
        {
            var sprite = new SimpleSprite(image);
            GameElement newElement;

            switch (elementType)
            {
                case GameElementType.Door:
                    newElement = new Door(location, elementType) { Sprite = sprite };
                    break;
                case GameElementType.PushableBlock:
                    newElement = new GameElement(location, elementType) { Sprite = sprite };
                    break;
                case GameElementType.ActionBlock:
                    newElement = new ActionBlock(location, elementType) { Sprite = sprite };
                    break;
                case GameElementType.CollectibleItem:
                    newElement = new GameElement(location, elementType) { Sprite = sprite };
                    break;
                default:
                    newElement = new GameElement(location, elementType) { Sprite = sprite };
                    break;
            }

            if (elementType == GameElementType.Door)
            {
                var door = newElement as Door;
                door.DeactiveStateSprite = sprite;

                if (isExitDoor)
                {
                    if(exitDoor[0] != null)
                        exitDoor[0] = door;
                    else exitDoor[1] = door;
                }
            }

            if(newElement != null)
                MapElements[layer].Add(newElement);
        }

        GameElement? GetElementByLocation(Point location, int layer)
        {
            return MapElements[layer].FirstOrDefault(m => m.Location == location) ?? null;
        }

        public void EndGame()
        {

        }

        // Don't use IntersectWith because it's too accurate (tile edge issues)
        public bool IsColliding(Rect a)
        {
            return MapElements.Any(m => m.Any(e => !e.IsPermeable && a == e.Area));
        }

        Point ParsePoint(string data)
        {
            var point = Point.Parse(data);
            return new Point(point.Y * 32, point.X * 32);
        }
    }
}
