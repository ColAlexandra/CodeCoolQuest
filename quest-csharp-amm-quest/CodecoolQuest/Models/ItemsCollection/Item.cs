using System.Diagnostics;

namespace Codecool.Quest.Models.ItemsCollection
{
    public abstract class Item : IDrawable
    {
        public Cell Cell { get; private set; }
        public int Bonus { get; set; }

        public int X => Cell.X;

        public int Y => Cell.Y;

        public abstract string TileName { get; }

        protected Item(Cell cell)
        { 
            Cell = cell;
            Cell.Items = this;
        }

    }
}
