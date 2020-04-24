using System.Diagnostics;

namespace Codecool.Quest.Models.Actors
{
    public abstract class Actor : IDrawable
    {
        //kazdy actor mogl posiadac ekwipunek, lista ekwipunku, kazda z klas wykorzystuje ekwipunek inaczej//jesli szkielet zginie to upusci ekwipunek//zabic szkielet, zeby upuscil
        //todo: create an inventory of actor
        //todo: 
        public Cell Cell { get; private set; }
        public int Health { get; set; } = 10;

        public int X => Cell.X;

        public int Y => Cell.Y;

        public abstract string TileName { get; }

        protected Actor(Cell cell)
        {
            Cell = cell;
            Cell.Actor = this;
        }

        public void Move(int dx, int dy)
        {
            var nextCell = Cell.GetNeighbor(dx, dy);

            Cell.Actor = null;
            //this = actor na ktorym wywołujemy metode move 
            nextCell.Actor = this;
            Cell = nextCell;

        }

    }
}