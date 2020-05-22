using System;
using System.Collections.Generic;
using System.Text;

namespace TheGauntlet.Overworld
{
    public class OverworldGrid
    {
		public Character Character { get; set; }
        public Tile[,] Tiles { get; set; }

		private readonly Random _random;

        private const int _gridWidth = 11;
        private const int _gridLength = 11;
		private const int _numChests = 20;

        public OverworldGrid(Random random)
		{
			Tiles = new Tile[_gridWidth, _gridLength];
            
            _random = random;

			Character = new Character()
            { 
                Position = new TileCoordinate(5, 5)
            };

            for (int i = 0; i < _gridWidth; i++)
            {
                for (int j = 0; j < _gridLength; j++)
                {
                    Tiles[i, j] = new Tile();
                }
            }

            Tiles[5, 5].Element = Character;

			PlaceChests();
		}

        public void Display()
        {
            Console.Clear();
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(@" __________________________________      
    |                               |    ");
            for (int i = 0; i < _gridWidth; i++)
            {
                Console.Write("    |    ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("|");
                for (int j = 0; j < _gridLength; j++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(Tiles[j, i].DisplayCharacter);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("|");
                }
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("    |    ");
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(@"    |                               |    
    |                               |    
    |_______________________________|    ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void MoveCharacter(int right, int down)
        {
            var newPosition = Character.Position + (right, down);
            if (newPosition.X >= 0 && newPosition.X < _gridWidth && newPosition.Y >= 0 && newPosition.Y < _gridLength)
            {
                if (Tiles[newPosition.X, newPosition.Y].Element is Chest chest)
                {
                    Character.Inventory.Add(chest.Item);
                }

                Tiles[Character.Position.X, Character.Position.Y].Element = new TrampledTile() 
                {
                    Position = Character.Position
                };

                Character.Position = newPosition;
                Tiles[Character.Position.X, Character.Position.Y].Element = Character;
            }
        }

        public void PlaceChests()
        {
            Chest chest;

            for (int i = 0; i < _numChests; i++)
			{
                int randX = _random.Next(0, _gridWidth);
                int randY = _random.Next(0, _gridLength);

                if (Tiles[randX, randY].Element == null)
				{
					chest = new Chest()
                    {
                        Position = new TileCoordinate(randX, randY)
                    };
                    Tiles[randX, randY].Element = chest;

					int itemType = _random.Next(1, 5);

                    chest.Item = itemType switch
                    {
                        1 => ItemContainer.AttackBoost,
                        2 => ItemContainer.DefenceBoost,
                        3 => ItemContainer.HealthBoost,
                        _ => ItemContainer.SpeedBoost
                    };
				}
			}  
		}
    }
}
