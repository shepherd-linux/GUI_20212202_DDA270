using Cirmi.Models;
using Cirmi.SpriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cirmi.Logics
{
    public interface IMapManagerLogic
    {
        public List<List<GameElement>> MapElements { get; set; }
        public List<Floor> FloorElements { get; set; }
        public bool IsColliding(Rect a);
        public void EndGame();
    }
}
