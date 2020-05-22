using System;
using System.Collections.Generic;
using System.Text;

namespace TheGauntlet.Overworld
{
    public class Chest : TileElement
    {
        public override char DisplayCharacter => '*';
        public Item Item { get; set; }
    }
}
