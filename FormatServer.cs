using Codecool.Battleship.DataModel;
using System;

namespace Codecool.Battleship.FormatServer
{
	public class Square {
		public string _status;
		public Square() {
			_status = "*";
		}
		public override string ToString() {
		}
	}
	public class Board {
		public int _size;
		private Square[,] map;
		public Board(int size) 
		{
			_size = size;
			Square[,] map = new Square[_size, _size];
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
					map[row, col]._status = "*";
				}
			}
		}
		public void PlaceShips(Player player) 
		{
			foreach (Ship ship in player.ships)
            {
				foreach (Location location in ship.FieldMap())
                {
					map[location.Item1, location.Item2]._status = "B";
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
						map[row, col]._status = "H";
                    }
					else if (player.misses.Contains(location))
					{
						map[row, col]._status = "M";
					}
					else if (player.sunks.Contains(location))
					{
						map[row, col]._status = "S";
					}
				}

		}
		public override string ToString() {
		}
		public bool ValidLocation(Location location) // checks if the location exists
        {
			if (location.Item1 >=0 && location.Item1 < _size && location.Item2 >= 0 && location.Item2 < _size)
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
		public void Show(string msg) //print board
		{
			string tableStart = "   ";
			for (int row = 0; row < _size; row++)
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
			Console.WriteLine(tableStart);
			string breakLine = "  -" + new string('-', 4 * _size);
			Console.WriteLine(breakLine);

			for (int row = 0; row < _size; row++)
			{
				string rowStart = $"{(char)('A' + row)} |";
				Console.Write(rowStart);
				for (int col = 0; col < _size; col++)
				{
					Console.Write(" ");
					Console.Write(board[row, col]._status);
					Console.Write(" |");
				}
				Console.WriteLine();
				Console.WriteLine(breakLine);
			}
		}
	}
	public class Input {
		public Location ReadLocation() //get the coordinates from the player ex A3 
		{
			int row;
			int col;
			bool flag = true;
			while (flag)
			{
				string input = Console.ReadLine();
				row = Convert.ToInt32(input[0].ToString().ToUpper()) - 'A';
				if (row < 0 || row >= _size)
                {
                    Console.WriteLine("Invalid coordinates!");
					continue;
				}
				if (!int.TryParse(input.Substring(1), out col))
				{
					Console.WriteLine("Invalid coordinates!");
					continue;
				}
				if (col < 0 || col >= _size) continue;
				{
					Console.WriteLine("Invalid coordinates!");
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
				return -1;
            }
		}
		public string ReadString() 
		{
			string input = Console.ReadLine();
			return input;
		}
	}
}