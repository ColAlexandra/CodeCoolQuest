﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.Quest.Models.ItemsCollection
{
    public class Sword : Item
    {
        public override string TileName => "sword";

        public Sword(Cell cell) : base(cell)
        {


        }
    }
}
