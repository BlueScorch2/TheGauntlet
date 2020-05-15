using System;
using System.Collections.Generic;
using System.Text;

namespace TheGauntlet.Overworld
{
    abstract class TileElement
    {
        public char DisplayCharacter { get; set; }

        private TileCoordinate _position;
        public TileCoordinate Position 
        {
            get => _position;
            set 
            {
                _position.X = Math.Clamp(value.X, 0, 9);
                _position.Y = Math.Clamp(value.Y, 0, 9);
            } 
        };
    }
}
