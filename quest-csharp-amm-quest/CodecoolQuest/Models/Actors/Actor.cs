﻿using System.Diagnostics;

namespace Codecool.Quest.Models.Actors
{
    public abstract class Actor : IDrawable
    {
        public Cell Cell { get; private set; }
        public int Health { get; set; } = 10;

        public int X => Cell.X;

        public int Y => Cell.Y;

        public abstract string TileName { get; }

        public bool Passable => Health < 0;
        protected Actor(Cell cell)
        {
            Cell = cell;
            Cell.Actor = this;
        }

        public void Move(int dx, int dy)
        {
            var nextCell = Cell.GetNeighbor(dx, dy);
            if (nextCell.Passable())
            {
                Cell.Actor = null;
                //this = actor na ktorym wywołujemy metode move 
                nextCell.Actor = this;
                Cell = nextCell;
            }
        }

    }
}