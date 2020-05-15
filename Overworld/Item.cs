using System;
using System.Collections.Generic;
using System.Text;
using TheGauntlet.Battle;

namespace TheGauntlet.Overworld
{
    public class Item
    {
		public string Name { get; set; }
		public Action<Combatant> OnApply { get; set; }

        public Item(string name, Action<Combatant> onApply)
        {
            Name = name;
            OnApply = onApply;
        }
    }
}
