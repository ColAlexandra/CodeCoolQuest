using Codecool.Quest.Models.Actors;
using Codecool.Quest.Models.ItemsCollection;

namespace Codecool.Quest.Models
{
    public class Cell : IDrawable
    {
        //todo: add properties to neighbour: up, down, ect.
        //todo: add method retun neighboor after recivin move direction
        public Actor Actor { get; set; }

        public Item Items { get; set; }

        public CellType CellType { get; set; }

        public int X { get; }
        public int Y { get; }

        public string TileName => CellType.ToString("g").ToLowerInvariant();

        private readonly GameMap _gameMap;

       


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

        public bool CellTakenByActor()
        {
            return this.Actor == null && this.CellType == CellType.Floor;
        }

        public bool CellTakenByItem()
        {
            return this.Items == null && this.CellType == CellType.Floor; 
        }

        public Actor ActorInCell()
        {
            return this.Actor;
        }

        public Item ItemInCell()
        {
            return this.Items;
        }
    }
}