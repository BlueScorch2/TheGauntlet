using System;
using System.Collections.Generic;
using System.Text;

namespace TheGauntlet.Overworld
{
    public class TrampledTile : TileElement
    {
        public override char DisplayCharacter => Element?.DisplayCharacter ?? '+';
        public TileElement Element { get; set; }
    }
}
