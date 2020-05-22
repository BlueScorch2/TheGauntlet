using System;
using System.Collections.Generic;
using System.Text;

namespace TheGauntlet.Overworld
{
    public struct TileCoordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public TileCoordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static TileCoordinate operator +(TileCoordinate left, (int X, int Y) right)
        {
            return new TileCoordinate(left.X + right.X, left.Y + right.Y);
        }

        public static TileCoordinate operator -(TileCoordinate left, (int X, int Y) right)
        {
            return new TileCoordinate(left.X - right.X, left.Y - right.Y);
        }
    }
}
