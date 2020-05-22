using System;
using System.Collections.Generic;
using System.Text;

namespace TheGauntlet.Overworld
{
    public class Chest : TileElement
    {
        public override char DisplayCharacter => 'C';
        public Item Item { get; set; }
    }
}
