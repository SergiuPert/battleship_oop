using Codecool.Battleship.DataModel;
using System;
using System.Collections.Generic;

namespace Codecool.Battleship.FormatServer
{
	public class Square {
		public string _status;
		public Square() {
			_status = "*";
		}
		public override string ToString() 
		{
			return _status;
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
				foreach (Location location in ship.GetFieldMap())
                {
					map[location.x, location.y]._status = "B";
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
					if (LocationInList(location, player.hits))
                    {
						map[row, col]._status = "H";
                    }
					else if (LocationInList(location, player.misses))
					{
						map[row, col]._status = "M";
					}
					else if (LocationInList(location, player.sunks))
					{
						map[row, col]._status = "S";
					}
				}
		}
		public bool LocationInList(Location location, List<Location> locations)
        {
			foreach(Location target in locations)
            {
				if (target.x == location.x && target.y == location.y)
                {
					return true;
                }
            }
			return false;
        }
		public override string ToString() //print board
		{
			string msg = "";
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
			//Console.WriteLine(tableStart);
			msg += tableStart + "\n";
			string breakLine = "  -" + new string('-', 4 * _size);
			//Console.WriteLine(breakLine);
			msg += breakLine + "\n";
			for (int row = 0; row < _size; row++)
			{
				string rowStart = $"{(char)('A' + row)} |";
				//Console.Write(rowStart);
				msg += rowStart;
				for (int col = 0; col < _size; col++)
				{
					//Console.Write(" ");
					msg += " ";
					//Console.Write(board[row, col]._status);
					msg += map[row, col]._status;
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
			if (location.x >=0 && location.x < _size && location.y >= 0 && location.y < _size)
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