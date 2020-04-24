using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.Quest.Models.Actors
{
    public class Door : Actor
    {
        public override string TileName => "door";

        public Door(Cell cell) : base(cell)
        {
        }


    }
}
