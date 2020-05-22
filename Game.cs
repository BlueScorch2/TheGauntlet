using System;
using System.Collections.Generic;
using System.Text;
using TheGauntlet.Battle;
using TheGauntlet.Overworld;
using System.Linq;

namespace TheGauntlet
{
    public class Game
    {
        private BattleController _battleController;
        private GameState _gameState;
        private OverworldGrid _overworldGrid;
        private int _maximumMoves;
        private int _moves;
        private Combatant _playerCombatant;
        private readonly Random _random = new Random();

        public void BeginBattle()
        {
            Console.WriteLine("Opening chests...");
            Console.ForegroundColor = ConsoleColor.Green;

            foreach (var item in _overworldGrid.Character.Inventory)
            {
                item.OnApply(_playerCombatant);
            }

            Console.WriteLine($"\nFinal Stats\nHealth: {_playerCombatant.Health}\nAttack: {_playerCombatant.Attack}\nDefence: {_playerCombatant.Defence}\nSpeed: {_playerCombatant.Speed}\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Press any key to continue...");
            Console.ReadKey();

            _gameState = GameState.Battle;
            _battleController = new BattleController(this, _playerCombatant, _random);
            _battleController.Loop();
        }

        private void BeginOverworld()
        {
			_gameState = GameState.Overworld;
            _overworldGrid.Display();
            Console.Write($"Moves left: {_maximumMoves}");
            do
            {
                GetInput();
            }
            while (_moves < _maximumMoves);

            Console.WriteLine("You have run out of moves!\nPress any key to continue to the battle...");
            Console.ReadKey();
            Console.Clear();
            BeginBattle();
        }

        private void ChooseClass()
        {
			_gameState = GameState.ChoosingClass;
            Console.WriteLine(@"Choose your class:
[1]: Tank
[2]: Ninja
[3]: Mage
[4]: Dark Mage
");
            GetInput();
        }

        public void GetInput()
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            if (_gameState == GameState.ChoosingClass)
			{
				ConsoleKey[] acceptableKeys = { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4 };
				bool finished = false;
                const int baseHealth = 500;

                while (!finished)
				{
					key = Console.ReadKey(true);
                    if (acceptableKeys.Contains(key.Key))
					{
						finished = true;
					}
				}

                switch (key.Key)
                {
                    case ConsoleKey.D1:
						_playerCombatant = new Combatant(random: _random)
						{
							MaxHealth = baseHealth + 80,
							Attack = 6,
							Defence = 9,
							Speed = 2,
							Name = "Tank",
                            Moves = new List<Move>()
                            {
                                MoveContainer.Punch,
                                MoveContainer.BodySlam,
                                MoveContainer.Toughen,
                                MoveContainer.UltimateSlam
                            }
                        };
                        break;
                    case ConsoleKey.D2:
                        _playerCombatant = new Combatant(random: _random)
                        {
                            MaxHealth = baseHealth + 40,
                            Attack = 8,
                            Defence = 3,
                            Speed = 10,
                            Name = "Ninja",
                            Moves = new List<Move>()
                            {
                                MoveContainer.Punch,
                                MoveContainer.BodySlam,
                                MoveContainer.Toughen,
                                MoveContainer.Shinobi
                            }
                        };
                        break;
                    case ConsoleKey.D3:
                        _playerCombatant = new Combatant(random: _random)
                        {
                            MaxHealth = baseHealth + 60,
                            Attack = 8,
                            Defence = 5,
                            Speed = 6,
                            Name = "Mage",
                            Moves = new List<Move>()
                            {
                                MoveContainer.Punch,
                                MoveContainer.BodySlam,
                                MoveContainer.Toughen,
                                MoveContainer.Fireball
                            }
                        };
                        break;
                    case ConsoleKey.D4:
                        _playerCombatant = new Combatant(random: _random)
                        {
                            MaxHealth = baseHealth + 90,
                            Attack = 9,
                            Defence = 2,
                            Speed = 5,
                            Name = "Dark Mage",
                            Moves = new List<Move>()
                            {
                                MoveContainer.Punch,
                                MoveContainer.BodySlam,
                                MoveContainer.Toughen,
                                MoveContainer.LifeDrain
                            }
                        };
                        break;
                }
                const int baseMaximumMoves = 0;
                _maximumMoves = 10 + _playerCombatant.Speed;
            }

            else if (_gameState == GameState.Overworld)
            {

				ConsoleKey[] acceptableKeys = { ConsoleKey.W, ConsoleKey.A, ConsoleKey.S, ConsoleKey.D };
				bool finished = false;

				while (!finished)
				{
					key = Console.ReadKey(true);
					if (acceptableKeys.Contains(key.Key))
					{
						finished = true;
					}
				}

				switch (key.Key)
                {
                    case ConsoleKey.W:
                        _overworldGrid.MoveCharacter(0, -1);
                        _moves++;
                        _overworldGrid.Display();
                        Console.WriteLine($"Moves left: {_maximumMoves - _moves}");
                        break;
                    case ConsoleKey.A:
                        _overworldGrid.MoveCharacter(-1, 0);
                        _moves++;
                        _overworldGrid.Display();
                        Console.WriteLine($"Moves left: {_maximumMoves - _moves}");
                        break;
                    case ConsoleKey.S:
                        _overworldGrid.MoveCharacter(0, 1);
                        _moves++;
                        _overworldGrid.Display();
                        Console.WriteLine($"Moves left: {_maximumMoves - _moves}");
                        break;
                    case ConsoleKey.D:
                        _overworldGrid.MoveCharacter(1, 0);
                        _moves++;
                        _overworldGrid.Display();
                        Console.WriteLine($"Moves left: {_maximumMoves - _moves}");
                        break;
                }
            }

            else
            {
				ConsoleKey[] acceptableKeys = { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4 };
				bool finished = false;

				while (!finished)
				{
					key = Console.ReadKey(true);
					if (acceptableKeys.Contains(key.Key))
					{
						finished = true;
					}
				}

                _battleController.SetPlayerAttack(key.Key switch
                { 
                    ConsoleKey.D1 => 0,
                    ConsoleKey.D2 => 1,
                    ConsoleKey.D3 => 2,
                    ConsoleKey.D4 => 3
                });
            }
        }

        public void Start()
        {
            _overworldGrid = new OverworldGrid(_random);
            ChooseClass();
            BeginOverworld();
        }
    }
}
