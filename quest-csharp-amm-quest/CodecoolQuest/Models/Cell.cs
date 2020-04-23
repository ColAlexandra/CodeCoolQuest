using System;
using Codecool.Quest.Models.Actors;
using Codecool.Quest.Models.ItemsCollection;
using Codecool.Quest.Models;

namespace Codecool.Quest.Models
{
    public class Cell : IDrawable
    {
        public Actor Actor { get; set; }

        public Items Items { get; set; }

        public Button Button { get; set; }

        public CellType CellType { get; set; }

        public int X { get; }
        public int Y { get; }

        public string TileName => CellType.ToString("g").ToLowerInvariant();

        private readonly GameMap _gameMap;

        public bool Passable()
        {
            if(CellType != CellType.Wall)
            {
                if (this.Actor != null)
                {
                    return this.Actor.Health < 0;
                }
                else
                {
                    return true;
                }
                
            }
            return false;
        }
        public Cell(GameMap gameMap, int x, int y, CellType cellType)
        {
            _gameMap = gameMap;
            X = x;
            Y = y;
            CellType = cellType;


        }
        

        public Cell GetNeighbor(int dx, int dy)
        {
            return _gameMap.GetCell(X + dx, Y + dy) ?? this;
        }
    }
}