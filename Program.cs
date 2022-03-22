using Codecool.Battleship.FormatServer;

namespace Codecool.Battleship
{
	class Program
	{
		static void Main(string[] args) {
			Input keyboard=new();
			Display screen = new();
			Battleship game = new(keyboard,screen);
			game.Info();
			for (; ; ) {
				game.Init();
				while (game.Winner() == 0) game.Play();
				screen.Show("New game?");
				if (keyboard.ReadString().ToLower() != "yes") break;
			}
		}
	}
}
