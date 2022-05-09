using Cirmi.Models;
using Cirmi.SpriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Cirmi.Logics
{
    internal class PlayerLogic : IPlayerLogic
    {
        public Player Player { get; set; }
        IMapManagerLogic mapLogic;
        IMenuManagerLogic menuManagerLogic;

        public void Setup(Point location, List<ImageSource> sprites, IMapManagerLogic _mapLogic, IMenuManagerLogic _menuManagerLogic)
        {
            Player = new Player(location);
            Player.Sprite = new CharacterSprite(sprites);
            mapLogic = _mapLogic;
            menuManagerLogic = _menuManagerLogic;
        }

        public void Move(MovementDirection direction)
        {
            Player.Sprite.Direction = (int)direction;
            Player.Sprite.StartAnimation();

            Point newLocation = CalculateNewLocation(direction, Player.Location);

            if (Player.Area != null)
            {
                var newRect = new Rect(newLocation.X, newLocation.Y, Player.Area!.Value.Width, Player.Area!.Value.Height);

                if (!mapLogic.IsColliding(newRect))
                {
                    Player.Location = newLocation;
                    mapLogic.CheckGameOver(Player.Score, Player.Location);
                }
                else
                {
                    //var collidedElement = mapLogic.MapElements[1].Find(e => e.Area == newRect);
                    var collidedElement = (from sublist in mapLogic.MapElements
                                           from item in sublist
                                           where item.Area == newRect
                                           select item).FirstOrDefault();

                    if (collidedElement != null && collidedElement.ElementType == GameElementType.PushableBlock)
                    {
                        var newBlockLocation = CalculateNewLocation(direction, collidedElement.Location);
                        var newBlockRect = new Rect(newBlockLocation.X, newBlockLocation.Y, 32, 32);

                        if (!mapLogic.IsColliding(newBlockRect))
                        {
                            (collidedElement as PushableBlock).Push(newBlockLocation);
                            Player.Location = newLocation;

                            var actionBlock = mapLogic.MapElements[1].Find(e => e.ElementType == GameElementType.ActionBlock && e.Area == newBlockRect);
                            if (actionBlock != null)
                                (actionBlock as ActionBlock).Activate();
                        }
                    }
                }
            }
        }

        private Point CalculateNewLocation(MovementDirection direction, Point location)
        {
            switch (direction)
            {
                case MovementDirection.Up:
                    return new Point(location.X, location.Y - 32);
                case MovementDirection.Right:
                    return new Point(location.X + 32, location.Y);
                case MovementDirection.Down:
                    return new Point(location.X, location.Y + 32);
                case MovementDirection.Left:
                    return new Point(location.X - 32, location.Y);
                default:
                    return new Point();
            }
        }

        public void OpenPauseMenu()
        {
            menuManagerLogic.LoadPauseMenu();
        }

        public void Interact()
        {
            var collidedElement = mapLogic.MapElements[1].Find(e => e.Area == Player.Area!.Value);

            if (collidedElement != null)
            {
                if (collidedElement.ElementType == GameElementType.ActionBlock)
                {
                    var item = Player.Inventory.FirstOrDefault(i => i.ItemType == CollectableItemType.Placeable);
                    if (item != null)
                    {
                        item.Area = Player.Area!.Value;
                        item.Location = Player.Location;
                        mapLogic.MapElements[1].Add(item);
                        Player.Inventory.Remove(item);
                        (collidedElement as ActionBlock).Activate();
                    }
                }
                else if (collidedElement.ElementType == GameElementType.CollectibleItem)
                {
                    Player.Inventory.Add(collidedElement as CollectableItem);
                    mapLogic.MapElements[1].Remove(collidedElement);
                }
            }
            else
            {
                var door = mapLogic.GetClosestDoor(Player.Area!.Value);

                if (door != null && !door.IsExitDoor)
                {
                    var key = Player.Inventory.FirstOrDefault(i => i.ItemType == CollectableItemType.Key);

                    if (key != null)
                    {
                        door.Open();
                        Player.Inventory.Remove(key);
                    }
                }
                else
                {
                    var item = Player.Inventory.FirstOrDefault(i => i.ItemType == CollectableItemType.Placeable);
                    if (item != null)
                    {
                        item.Area = Player.Area!.Value;
                        item.Location = Player.Location;
                        mapLogic.MapElements[1].Add(item);
                        Player.Inventory.Remove(item);
                    }
                }
            }
        }
    }
}
