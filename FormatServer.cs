using Codecool.Battleship.DataModel;
using System;

namespace Codecool.Battleship.FormatServer
{
	public class Square {
		public string status;
		public Square() {
			status = "*";
		}
		public override string ToString() 
		{
			return status;
		}
	}
	public class Board {
		public int size { get; set; }
		private Square[,] map;
		public Board(int Size) 
		{
			size = size;
			Square[,] map = new Square[size, size];
			for (int row = 0; row < map.GetLength(0); row++)
            {
				for (int col = 0; col < map.GetLength(1); col++)
				{
					map[row, col] = new Square();
                }
            }

		}
		public void Reset() 
		{
			for (int row = 0; row < map.GetLength(0); row++)
			{
				for (int col = 0; col < map.GetLength(1); col++)
				{
					map[row, col].status = "*";
				}
			}
		}
		public void PlaceShips(Player player) 
		{
			foreach (Ship ship in player.ships)
            {
				foreach (Location location in ship.GetFieldMap())
                {
					map[location.x, location.y].status = "B";
                }
            } 
		}
		public void PlaceMoves(Player player) //places all the player moves on the board
		{
			Location location;
			for (int row = 0; row < map.GetLength(0); row++)
				for (int col = 0; col < map.GetLength(1); col++)
                {
					location = new Location(row, col);
					if (player.hits.Contains(location))
                    {
						map[row, col].status = "H";
                    }
					else if (player.misses.Contains(location))
					{
						map[row, col].status = "M";
					}
					else if (player.sunks.Contains(location))
					{
						map[row, col].status = "S";
					}
				}

		}
		public override string ToString() //print board
		{
			string msg = "";
			string tableStart = "   ";
			for (int row = 0; row < size; row++)
			{
				if (row < 9)
				{
					tableStart += $" {row + 1}  ";
				}
				else
				{
					tableStart += $" {row + 1} ";
				}
			}
			//Console.WriteLine(tableStart);
			msg += tableStart + "\n";
			string breakLine = "  -" + new string('-', 4 * size);
			//Console.WriteLine(breakLine);
			msg += breakLine + "\n";
			for (int row = 0; row < size; row++)
			{
				string rowStart = $"{(char)('A' + row)} |";
				//Console.Write(rowStart);
				msg += rowStart;
				for (int col = 0; col < size; col++)
				{
					//Console.Write(" ");
					msg += " ";
					//Console.Write(board[row, col]._status);
					msg += map[row, col].status;
					//Console.Write(" |");
					msg += " |";
				}
				//Console.WriteLine();
				msg += "\n";
				//Console.WriteLine(breakLine);
				msg += breakLine + "\n";
			}
			return msg;
		}
		public bool ValidLocation(Location location) // checks if the location exists
        {
			if (location.x >=0 && location.x < size && location.y >= 0 && location.y < size)
            {
				return true;
            }
			else
            {
				return false;
            }
        }
	}
	public class Display {
		public void Show(string msg) 
		{
            Console.WriteLine(msg);
		}
		public void Clear()
        {
			Console.Clear();
        }
	}
	public class Input {
		public Location ReadLocation() //get the coordinates from the player ex A3 
		{
			int row = 0;
			int col = 0;
			bool flag = true;
			while (flag)
			{
                Console.WriteLine("Give us your coordinates: ");
				string input = Console.ReadLine();
				row = Convert.ToInt32(input[0].ToString().ToUpper()) - 'A';
				if (!int.TryParse(input.Substring(1), out col))
				{
					Console.WriteLine("Invalid format!");
					continue;
				}
				flag = false;
			}
			Location location = new Location(row, col);
			return location;
		}
		public int ReadInt() 
		{
			string input = Console.ReadLine();
			if (int.TryParse(input, out int value))
            {
				return value;
            }
			else
            {
                Console.WriteLine("Incorrrect format! We need an integer.");
				return ReadInt();
            }
		}
		public string ReadString() 
		{
			string input = Console.ReadLine();
			return input;
		}
	}
}