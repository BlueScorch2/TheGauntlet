using System;
using System.Collections.Generic;
using System.Text;

namespace TheGauntlet.Battle
{
    public class Move
    {
        public int Accuracy { get; }
        public string Description { get; }
        public string Name { get; }
        public int Power { get; }
        public Action<Combatant, Combatant, int> SpecialEffect { get; }

        public Move(string name, string description, int power, int accuracy, Action<Combatant, Combatant, int> specialEffect)
        {
            Accuracy = accuracy;
            Description = description;
            Name = name;
            Power = power;
            SpecialEffect = specialEffect;
        }
    }
}
