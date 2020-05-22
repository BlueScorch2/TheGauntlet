using System;
using System.Collections.Generic;
using System.Text;

namespace TheGauntlet.Overworld
{
    public abstract class TileElement
    {
        public abstract char DisplayCharacter { get; }

        private TileCoordinate _position;
        public TileCoordinate Position
        {
            get => _position;
            set
            {
                _position.X = Math.Clamp(value.X, 0, 10);
                _position.Y = Math.Clamp(value.Y, 0, 10);
            }
        }
    }
}
