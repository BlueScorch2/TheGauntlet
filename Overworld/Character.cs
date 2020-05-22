using System;
using System.Collections.Generic;
using System.Text;

namespace TheGauntlet.Overworld
{
    public class Character : TileElement
    {
        public override char DisplayCharacter => 'O';
        public List<Item> Inventory = new List<Item>();
        public int Speed { get; set; }
    }
}
