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

        private readonly Random _random;

        public Combatant(Random random)
        {
            _random = random;
        }

        public void DoMove(Combatant target, int moveIndex)
        {
            var move = Moves[moveIndex];
            if (target.StatusEffect == StatusEffect.Stun)
            {
                Console.WriteLine($"{Name} is stunned and can't move!");
                target.StatusEffect = StatusEffect.None;
            }
            else if (_random.Next(0, 100) < move.Accuracy)
            {
                int damage = (Attack - target.Defence / 2) * move.Power;

                if (damage <= 0 && move.Name != "Toughen")
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{Name} was barely able to hit {target.Name}");
                    Console.ForegroundColor = ConsoleColor.White;
                    damage = _random.Next(0, 50);
                }

                target.Health -= damage;

                Console.WriteLine($"{Name} used {move.Name}!\n{Name} dealt {damage} damage to {target.Name}!");

                move.SpecialEffect?.Invoke(this, target, damage);
            }
            else
            {
                Console.WriteLine($"{Name} used {move.Name}!\nIt missed!");
            }
        }
    }
}
