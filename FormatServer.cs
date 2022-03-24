using Codecool.Battleship.DataModel;
using System;
using System.Collections.Generic;

namespace Codecool.Battleship.FormatServer
{
	public class Square {
		public string status;
		public Square() {
			status = "≋";
		}
		public override string ToString() 
		{
			return status;
		}
	}
	public class Board {
		public int size { get; set; }
		public Square[,] map { get; set; }
		public Board(int Size)
		{
			size = Size;
			map = new Square[size, size];
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
					map[row, col].status = "≋";
				}
			}
		}
		public void PlaceShips(Player player) 
		{
			foreach (Ship ship in player.ships)
            {
				foreach (Location location in ship.GetFieldMap())
                {
					map[location.x, location.y].status = "⚓";
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
					if (location.LocationInList(player.hits))
                    {
						map[row, col].status = "♨";
                    }
					else if (location.LocationInList(player.misses))
					{
						map[row, col].status = "☀";
					}
					else if (location.LocationInList(player.sunks))
					{
						map[row, col].status = "☠";
					}
				}
		}
		public override string ToString() //print board
		{
			return "a";
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
		public void PrintBoard(Board board)
        {
			//string msg = "";
			string tableStart = "  ";
			for (int row = 0; row < board.size; row++)
			{
				if (row < 9)
				{
					tableStart += $" {row + 1} ";
				}
				else
				{
					tableStart += $" {row + 1}";
				}
			}
            Console.WriteLine(tableStart);
            //msg += tableStart + "\n";
            //string breakLine = "  -" + new string('-', 4 * size);
            //Console.WriteLine(breakLine);
            //msg += breakLine + "\n";
            for (int row = 0; row < board.size; row++)
			{
				string rowStart = $"{(char)('A' + row)} ";
                Console.Write(rowStart);
                //msg += rowStart;
				for (int col = 0; col < board.size; col++)
				{
                    Console.Write(" ");
					//msg += " ";
					switch (board.map[row, col].status)
					{
						case "☀":
							Console.ForegroundColor = ConsoleColor.Blue;
							break;
						case "☠":
							Console.ForegroundColor = ConsoleColor.Red;
							break;
						case "♨":
							Console.ForegroundColor = ConsoleColor.Red;
							break;
						case "≋":
							Console.ForegroundColor = ConsoleColor.Blue;
							break;
						default:
							Console.ForegroundColor = ConsoleColor.White;
							break;
					}
					Console.Write(board.map[row, col].status);
					Console.ForegroundColor = ConsoleColor.White;
					//msg += board[row, col].status;
					Console.Write(" ");
                    //msg += " |";
				}
                Console.WriteLine();
                //msg += "\n";
                //Console.WriteLine(breakLine);
                //msg += breakLine + "\n";
            }
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
				if (input == "") continue;
				row = (int)(input[0].ToString().ToUpper())[0] - 'A';
				if (!int.TryParse(input.Substring(1), out col))
				{
					Console.WriteLine("Invalid format!");
					continue;
				}
				flag = false;
			}
			Location location = new Location(row, col-1);
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