using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.Quest
{
    public static class Utils
    {
        public static (int, int) ToVector(this MoveDirection move)
        {
            return move switch
            {
                MoveDirection.Right => (1, 0),
                MoveDirection.Left => (-1, 0),
                MoveDirection.Up => (0, -1),
                MoveDirection.Down => (0, 1),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
