using System;
using System.Collections.Generic;
using System.Text;

namespace TheGauntlet.Overworld
{
    public class Character : TileElement
    {
        public char DisplayCharacter { get; set; }
        public List<Item> Inventory { get; set; }
        public int Speed { get; set; }
    }
}
