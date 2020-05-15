using System;
using System.Collections.Generic;
using System.Text;

namespace TheGauntlet.Battle
{
    public class BattleController
    {
        public Combatant Enemy;
        public Combatant Player;
        private (int lower, int upper) _statBounds;
        private Random _random;
        private int _round;

        public BattleController(Combatant player, Random random)
        {
            Player = player;
            _statBounds = (3, 6);
            _random = random;
            _round = 0;

            StartNewBattle();
        }

        private static void ApplyStatusEffect(Combatant combatant)
        {
            switch (combatant.StatusEffect)
            {
                case StatusEffect.Burn:
                    {
                        int damage = combatant.MaxHealth / 10;
                        combatant.Health -= damage;
                        Console.WriteLine($"{combatant.Name} lost {damage} health due to burn!");
                        break;
                    }
                case StatusEffect.Poison:
                    {
                        int damage = combatant.MaxHealth / 10;
                        combatant.Health -= damage;
                        Console.WriteLine($"{combatant.Name} lost {damage} health due to poison!");
                        break;
                    }
                case StatusEffect.Regen:
                    {
                        int damage = combatant.MaxHealth / 10;
                        combatant.Health += damage;
                        Console.WriteLine($"{combatant.Name} regained {damage} health due to regen!");
                        break;
                    }
            }
        }

        private void ApplyStatusEffects()
        {
            ApplyStatusEffect(Player);
            ApplyStatusEffect(Enemy);
        }

        private void CreateEnemy()
        {
            Enemy = new Combatant()
            {
                MaxHealth = _random.Next(_statBounds.lower * 5, _statBounds.upper * 5),
                Attack = _random.Next(_statBounds.lower, _statBounds.upper),
                Defence = _random.Next(_statBounds.lower, _statBounds.upper),
                Speed = _random.Next(_statBounds.lower, _statBounds.upper)
            };
        }

        private void DisplayPlayerAttacks()
        {
            var output = new StringBuilder();
            for (int i = 0; i < Player.Moves.Count; i++)
            {
                output.AppendLine($"[{i}] {Player.Moves[i].Name}: (Power: {Player.Moves[i].Power}, Accuracy: {Player.Moves[i].Accuracy}) {Player.Moves[i].Description})");
            }
            Console.WriteLine(output.ToString());
        }

        private void EnemyAttack()
        {
            Enemy.DoMove(Player, _random.Next(0, Enemy.Moves.Count));
        }

        public void Loop()
        {
            do
            {
                DisplayPlayerAttacks();

                PlayerAttack();

                EnemyAttack();

                ApplyStatusEffects();

                if (Enemy.Health == 0)
                {
                    Console.WriteLine($"{Enemy.Name} was killed!");
                    StartNewBattle();
                }
            }
            while (Player.Health != 0);

            Console.WriteLine("You died!");
        }

        private void PlayerAttack()
        {
            Console.WriteLine("Enter attack: ");
            bool inputParsed;
            int moveIndex;
            string input = Console.ReadLine();
            do
            {
                inputParsed = int.TryParse(input, out moveIndex);
            }
            while (!inputParsed);
            Player.DoMove(Enemy, moveIndex);
        }

        private void StartNewBattle()
        {
            _round++;
            CreateEnemy();
        }
    }
}
