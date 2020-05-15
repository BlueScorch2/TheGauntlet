using System;
using System.Collections.Generic;
using System.Text;
using TheGauntlet.Battle;
using TheGauntlet.Overworld;

namespace TheGauntlet
{
    class Game
    {
		public GameState GameState { get; set; }

		private BattleController _battleController;
		private OverworldGrid _overworldGrid;
		private Combatant _playerCombatant;
		private Random _random;

        public void BeginBattle()
		{
			foreach (var item in _overworldGrid.Character.Inventory)
			{
                item.OnApply(_playerCombatant);
			}

            _battleController = new BattleController(_playerCombatant, _random);
            _battleController.Loop();
		}
		private void ChooseClass()
        {
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
			key = Console.ReadKey(true);

			if (GameState == GameState.ChoosingClass)
			{
				switch(key.Key)
				{
                    case ConsoleKey.D1:
                        _playerCombatant = new Combatant()
                        {
							Health = 80,
							Attack = 6,
							Defence = 9,
							Speed = 2,
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
                        _playerCombatant = new Combatant()
                        {
                            Health = 40,
                            Attack = 8,
                            Defence = 3,
                            Speed = 10,
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
                        _playerCombatant = new Combatant()
                        {
                            Health = 60,
                            Attack = 8,
                            Defence = 5,
                            Speed = 6,
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
                        _playerCombatant = new Combatant()
                        {
                            Health = 90,
                            Attack = 9,
                            Defence = 2,
                            Speed = 5,
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
			}
			
			else if (GameState == GameState.Overworld)
			{
				switch(key.Key)
				{
					case ConsoleKey.W:
						_overworldGrid.Character.Position.Y -= 1;
						break;
					case ConsoleKey.A:
						_overworldGrid.Character.Position.X -= 1;
						break;
					case ConsoleKey.S:
						_overworldGrid.Character.Position.Y +=  1;
						Overworld.
						break;
					case ConsoleKey.D:
						_overworldGrid.Character.Position.X += 1;
						break;
				}

                _overworldGrid.Display();
			}

			else
			{
				switch(key.Key)
				{
					case ConsoleKey.D1:
						break;
					case ConsoleKey.D2:
						break;
					case ConsoleKey.D3:
						break;
					case ConsoleKey.D4:

						break;
				}
			}
		}

        public void Start()
		{
			_overworldGrid = new overworldGrid();
            ChooseClass();
		}
}
