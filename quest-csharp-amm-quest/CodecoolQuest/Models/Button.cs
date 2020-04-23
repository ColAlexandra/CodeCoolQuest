using System;
using System.Collections.Generic;
using System.Text;
using Codecool.Quest.Models;
using Microsoft.Xna.Framework.Input;

namespace Codecool.Quest.Models
{
    public class Button : IDrawable
    {
        public Cell Cell { get; private set; }

        public int X => Cell.X;
        public int Y => Cell.Y;

        public string TileName => "button";

        public Button(Cell cell)
        {
            Cell = cell;
            Cell.Button = this;
        }

    }
}
