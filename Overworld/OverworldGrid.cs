using System;
using System.Collections.Generic;
using System.Text;

namespace TheGauntlet.Overworld
{
    public class OverworldGrid
    {
		public Character Character { get; set; }
		public Chest[] Chests { get; set; } = new Chest[_numChests];
        public Tile[,] Tile { get; set; }

		private Random _random;

        private const int gridWidth = 10;
        private const int gridLength = 10;
		private const int _numChests = 20;

        public OverworldGrid()
		{
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridLength; j++)
				{
					Tile[i,j] = new Tile();
                    Tile[i, j].DisplayCharacter = '+';
				}
            }
		}

        public void Display()
        {
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridLength; j++)
                {
                    Console.WriteLine(Tile[i,j].DisplayCharacter);
                }
            }
        }

        public void PlaceChests()
        {
			int chests = 0;
            while(chests < _numChests)
			{
				int randX = _random.Next(0, 11);
				int randY = _random.Next(0, 11);

                for (int i = 0; i < Chests.Length; i++)
				{
                    if ((Chests[i].Position.X != randX) && (Chests[i].Position.Y != randY))
					{
						Chests[i] = new Chest();
                        Chests[i].DisplayCharacter = 'C';

                        chests++;

						int itemType = _random.Next(1, 5);

                        switch (itemType)
						{
							case 1:
								Chests[i].Item = ItemContainer.AttackBoost;
                                break;
                            case 2:
                                Chests[i].Item = ItemContainer.DefenceBoost;
                                break;
							case 3:
								Chests[i].Item = ItemContainer.HealthBoost;
								break;
                            case 4:
								Chests[i].Item = ItemContainer.SpeedBoost;
								break;
                            default:
                                break;
						}
					}
				}
                
			}
		}
    }
}
