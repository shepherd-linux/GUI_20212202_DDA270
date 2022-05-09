using Cirmi.Models;
using Cirmi.SpriteModels;
using Cirmi.UserControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cirmi.Logics
{
    public interface IMapManagerLogic
    {
        public List<List<GameElement>> MapElements { get; set; }
        public List<Floor> FloorElements { get; set; }
        public Stopwatch GameTimer { get; set; }
        public bool IsColliding(Rect a);
        public int SelectedLevel { get; set; }
        public int SelectedSkin { get; set; }
        public Door GetClosestDoor(Rect a);
        public void CheckGameOver(int score, Point location);
        public void LoadNextLevel();
        public event Action LevelLoaded;
    }
}
