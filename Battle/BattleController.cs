using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheGauntlet.Battle
{
    public class BattleController
    {
        public Combatant Enemy { get; set; }
        public Combatant Player { get; set; }

        private readonly Game _game;
        private int _moveIndex;
        private (int lower, int upper) _statBounds;
        private readonly Random _random;
        private int _round;

        public BattleController(Game game, Combatant player, Random random)
        {
            _game = game;
            Player = player;
            _statBounds = (3, 6);
            _random = random;
            _round = 0;

            StartNewBattle();
        }

        public void Display()
		{
			const int boxCharSize = 15; // This can be changed if it doesn't work well

			var display = new StringBuilder();

			// first line

			const string playerChar = ":]";
			const string enemyChar = ">:|";

			int playerLeftSpaces = (boxCharSize - playerChar.Length) / 2;
			int playerRightSpaces = (boxCharSize - playerChar.Length) / 2;

			int enemyLeftSpaces = (boxCharSize - enemyChar.Length) / 2;
            int enemyRightSpaces = (boxCharSize - enemyChar.Length) / 2;

			display.Append(string.Concat(Enumerable.Repeat(" ", playerLeftSpaces)));
			display.Append(playerChar);
			display.Append(string.Concat(Enumerable.Repeat(" ", playerRightSpaces)));

			display.Append(string.Concat(Enumerable.Repeat(" ", enemyLeftSpaces)));
			display.Append(playerChar);
			display.Append(string.Concat(Enumerable.Repeat(" ", enemyRightSpaces)));

			display.Append("\n");

			// second line

			playerLeftSpaces = (boxCharSize - Player.Name.Length) / 2;
			playerRightSpaces = (boxCharSize - Player.Name.Length) / 2;

			enemyLeftSpaces = (boxCharSize - Enemy.Name.Length) / 2;
			enemyRightSpaces = (boxCharSize - Enemy.Name.Length) / 2;

			display.Append(string.Concat(Enumerable.Repeat(" ", playerLeftSpaces)));
			display.Append(Player.Name);
			display.Append(string.Concat(Enumerable.Repeat(" ", playerRightSpaces)));

			display.Append(string.Concat(Enumerable.Repeat(" ", enemyLeftSpaces)));
			display.Append(Enemy.Name);
			display.Append(string.Concat(Enumerable.Repeat(" ", enemyRightSpaces)));

			display.Append("\n");

			// third line

			string playerHealth = $"Health: {Player.Health}";
			string enemyHealth = $"Health: {Enemy.Health}";

			playerLeftSpaces = (boxCharSize - playerHealth.Length) / 2;
			playerRightSpaces = (boxCharSize - playerHealth.Length) / 2;

			enemyLeftSpaces = (boxCharSize - enemyHealth.Length) / 2;
			enemyRightSpaces = (boxCharSize - enemyHealth.Length) / 2;

			display.Append(string.Concat(Enumerable.Repeat(" ", playerLeftSpaces)));
			display.Append(string.Concat(playerHealth));
			display.Append(Enumerable.Repeat(" ", playerRightSpaces));

			display.Append(Enumerable.Repeat(" ", enemyLeftSpaces));
			display.Append(enemyHealth);
			display.Append(string.Concat(Enumerable.Repeat(" ", enemyRightSpaces)));

			display.Append("\n");
			display.Append("\n");

			// attacks

			display.Append("Choose an attack:");

            for (int i = 0; i < Player.Moves.Count; i++)
			{
				var move = Player.Moves[i];
				display.Append($"[{i + 1}] {move.Name} (Power: {move.Power}, Accuracy: {move.Accuracy}%) {move.Description}");
			}

			Console.WriteLine(display);
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
            int baseHealth = 500;
            Enemy = new Combatant(random: _random)
            {
                Name = $"Enemy {_round}",
                MaxHealth = baseHealth + _random.Next(_statBounds.lower * 100, _statBounds.upper * 100),
                Attack = _random.Next(_statBounds.lower, _statBounds.upper),
                Defence = _random.Next(_statBounds.lower, _statBounds.upper),
                Speed = _random.Next(_statBounds.lower, _statBounds.upper),
                Moves = new List<Move>()
                {
                    MoveContainer.Punch,
                    MoveContainer.BodySlam,
                    MoveContainer.Toughen
                }
            };
        }

        private void DisplayPlayerAttacks()
        {
            var output = new StringBuilder();

            for (int i = 0; i < Player.Moves.Count; i++)
            {
                output.AppendLine($"[{i}] {Player.Moves[i].Name}: (Power: {Player.Moves[i].Power}, Accuracy: {Player.Moves[i].Accuracy}) {Player.Moves[i].Description})");
            }

            output.AppendLine("Enter attack: ");

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
                Console.Clear();

                Display();

                DisplayPlayerAttacks();

                _game.GetInput();

                if (Player.Speed > Enemy.Speed)
				{
                    PlayerAttack();
                    if (Enemy.Health == 0)
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine($"{Enemy.Name} was killed!");
						Console.ForegroundColor = ConsoleColor.White;

						StartNewBattle();
					}
					else
					{
						Console.WriteLine();

						EnemyAttack();

						ApplyStatusEffects();
					}
				}
                else
				{
					EnemyAttack();
                    PlayerAttack();
                    if (Enemy.Health == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{Enemy.Name} was killed!");
                        Console.ForegroundColor = ConsoleColor.White;

                        StartNewBattle();
                    }
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            while (Player.Health != 0);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You died!");
        }

        private void PlayerAttack()
        {
            Player.DoMove(Enemy, _moveIndex);
        }

        public void SetPlayerAttack(int moveIndex)
        {
            _moveIndex = moveIndex;
        }

        private void StartNewBattle()
        {
            _round++;
            _statBounds.lower += 1;
            _statBounds.upper += 1;
            CreateEnemy();
        }
    }
}
