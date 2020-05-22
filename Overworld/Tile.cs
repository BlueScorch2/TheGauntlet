using System;
using System.Collections.Generic;
using System.Text;

namespace TheGauntlet.Overworld
{
    public class Tile
    {
		public char DisplayCharacter => Element?.DisplayCharacter ?? '-'; 
		public TileElement Element { get; set; }
    }
}
