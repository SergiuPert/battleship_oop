using System;
using Codecool.Battleship.FormatServer;
using Codecool.Battleship.DataModel;
using System.Collections.Generic;
using System.IO;

namespace Codecool.Battleship
{
	public enum BoatType{
		Submarine = 1,
		Destroyer = 2,
		Cruiser = 3,
		Battleship = 4,
		Carrier = 5
	};
	public class Battleship {
		private readonly Input Keyboard;
		private readonly Display Screen;
		private readonly List<int> Fleet;
		private Board board;
		private Player[] player;
		private int currentPlayer;
		private void DefineFleet() {
			Screen.Show("How many ships do you wish to have?");
			int size = Keyboard.ReadInt();
			for (int i = 0; i < size; i++)
			{
				Screen.Show($"Please select desired type for ship {i + 1}:\n1.Submarine\n2.Destroyer\n3.Cruiser\n4.Battleship\n5.Carrier\n");
				switch (Keyboard.ReadInt()){
					case 1: { Fleet.Add((int)BoatType.Submarine); break; }
					case 2: { Fleet.Add((int)BoatType.Destroyer); break; }
					case 3: { Fleet.Add((int)BoatType.Cruiser); break; }
					case 4: { Fleet.Add((int)BoatType.Battleship); break; }
					case 5: { Fleet.Add((int)BoatType.Carrier); break; }
				}
			}
		}
		private void InitPlayers() {
			player = new Player[2];
			Screen.Show("Please entered desired game mode:\n1.PvP\n2.PvE\n3.EvP\n4EvE");
			int mode = Keyboard.ReadInt();
			if (mode % 2 == 0) ToggleAiPlayer(1);
			if (mode > 2) ToggleAiPlayer(0);
			for(int i = 0; i < 2; i++)
			{
				if (player[i].IsAI) {
					player[i] = new Player($"AiPlayer{i}");
					InitRandomFleet(player[i],board.size);
				}
				else {
					Screen.Show($"Player {i + 1}, please state your name.");
					player[i] = new Player(Keyboard.ReadString());
					Screen.Show("Do you wish to configure your ships manually(y/n)?");
					if (Keyboard.ReadString() != "y") InitRandomFleet(player[i],board.size);
					else {
						for(int j = 0; j < Fleet.Count; j++) {
							for(; ; ) {
								Screen.Show($"Please enter desired location of ship {j + 1}. Length of ship is {Fleet[j]}.");
								Location place = RequestCoords();
								Ship boat = RegisterShip(place, Fleet[j]);
								if (ValidateShip(player[i], boat)) { player[i].ships.Add(boat);break; }
								Screen.Show("Can't place a ship there in that orientation! Try again.");
							}
							board.PlaceShips(player[i]);
							Screen.Clear();
							Screen.Show(board.ToString());
						}
						Screen.Show($"Fleet configured for {player[i].Name}. Hit enter to continue.");
						Keyboard.ReadString();
					}
				}
			}
		}
		private void InitRandomFleet(Player player,int mapSize) {
			Random random = new();
			for (int j = 0; j < Fleet.Count; j++) {
				for (; ; ) {
					int x = random.Next(mapSize);
					int y = random.Next(mapSize);
					Location place = new(x, y);
					Ship boat = RegisterShip(place, Fleet[j]);
					if (ValidateShip(player,boat)) { player.ships.Add(boat); break; }
				}
			}
		}
		private bool ValidateShip(Player player,Ship boat)
		{
			List<Location> fields = new();
			foreach (Ship ship in player.ships) fields.AddRange(ship.GetShadowMap());
			foreach (Location tile in boat.GetFieldMap()) if (!board.ValidLocation(tile) || tile.LocationInList(fields)) return false;
			return true;
		}
		private Location RequestCoords() {
			for(; ; ) { 
				Location place = Keyboard.ReadLocation();
				if (board.ValidLocation(place)) return place;
				Screen.Show("Location out of bounds! Try again");
			}
		}
		private Ship RegisterShip(Location where,int size) {
			Screen.Show("Please choose a direction for this ship:\n1.Up\n2.Right\n3.Down\n4.Left");
			int dir=0;
			while (true) {
				dir = Keyboard.ReadInt();
				if (dir > 0 || dir < 5) break;
				Screen.Show("Please pick an option between 1 and 4!");
			}
			return new Ship(where, size, dir);
		}
		private int WinMessage(int who) {
			Screen.Show($"Congratulation {player[who].Name}. You won.");
			return who;
		}
		private void ToggleAiPlayer(int who) {
			if (who < 0 || who > 1) return;
			player[who].IsAI = !player[who].IsAI;
		}
		public Battleship(Input cin,Display cout) {
			Screen = cout;
			Keyboard = cin;
		}
		public void Info() {
			string[] readText = File.ReadAllLines("Battleship.inf");
			foreach (string line in readText) Screen.Show(line);
		}
		public void Init() {
			Screen.Show("Pick board size:");
			board = new(Keyboard.ReadInt());
			DefineFleet();
			InitPlayers();
			currentPlayer = 0;
		}
		public void Play() {
			Screen.Show(board.ToString());
			Screen.Show($"{player[currentPlayer].Name} pick your move.");
			Location guess = Keyboard.ReadLocation();
			int move = 0;
			if(board.ValidLocation(guess)) move=player[currentPlayer].CheckIfHit(guess,player[(currentPlayer+1)%2]);
			switch (move) {
				case 0: { Screen.Show("Invalid move. Try again.");break; }
				case 1: { Screen.Show("Missed. Go fish!"); break; }
				case 2: { Screen.Show("Ouch... you hit!"); break; }
				case 3: { Screen.Show("Grrr.... You sunk one..."); break; }
			}	
		}
		public int Winner() {
			if (player[0].ships.Count == 0) return WinMessage(1);
			if (player[1].ships.Count == 0) return WinMessage(0);
			return 0;
		}
	}
}