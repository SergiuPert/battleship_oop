using Codecool.Battleship.DataModel;

namespace Codecool.Battleship.FormatServer
{
	public class Square {
		public char status;
		public Square() {
		}
		public override string ToString() {
		}
	}
	public class Board {
		private int _size;
		private Square[,] map;
		public Board(int size) {
		}
		public void Reset() {
		}
		public void PlaceShips(Player player) {
		}
		public void PlaceMoves(Player player) {
		}
		public override string ToString() {
		}
	}
	public class Display {
		public void Show(string msg) {
		}
	}
	public class Input {
		public Location ReadLocation() {
		}
		public int ReadInt() {
		}
		public string ReadString() {
		}
	}
}