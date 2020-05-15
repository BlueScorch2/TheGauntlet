using System;
using System.Collections.Generic;
using System.Text;

namespace TheGauntlet.Battle
{
    public class Combatant
    {
        public int Attack { get; set; }
        public int Defence { get; set; }

        private int _health;
        public int Health
        {
            get => _health;
            set
            {
                _health = Math.Clamp(value, 0, _maxHealth);
            }
        }

        private int _maxHealth;
        public int MaxHealth 
        {
            get => _maxHealth;
            set
            {
                _health = _maxHealth = value;
            }
        }

        public List<Move> Moves { get; set; }
        public string Name { get; set; }
        public int Speed { get; set; }
        public StatusEffect StatusEffect { get; set; }

        public void DoMove(Combatant target, int moveIndex)
        {
            var move = Moves[moveIndex];

            int damage = (Attack - target.Defence / 2) * move.Power;

            target.Health -= damage;

            move.SpecialEffect(this, target, damage);

            Console.WriteLine($"{Name} dealt {damage} damage to {target.Name}!");
        }
    }
}
