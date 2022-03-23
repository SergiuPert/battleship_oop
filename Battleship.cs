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
		private int currentPlayer;
		private List<int> fleet;
		private Player[] player;
		private Board board;
		private Input keyboard;
		private Display screen;
		private void DefineFleet() {
			screen.Show("How many ships do you wish to have?");
			int size = keyboard.ReadInt();
			for (int i = 0; i < size; i++)
			{
				screen.Show($"Please select desired type for ship {i + 1}:\n1.Submarine\n2.Destroyer\n3.Cruiser\n4.Battleship\n5.Carrier\n");
				switch (keyboard.ReadInt()){
					case 1: { fleet.Add((int)BoatType.Submarine); break; }
					case 2: { fleet.Add((int)BoatType.Destroyer); break; }
					case 3: { fleet.Add((int)BoatType.Cruiser); break; }
					case 4: { fleet.Add((int)BoatType.Battleship); break; }
					case 5: { fleet.Add((int)BoatType.Carrier); break; }
				}
			}
		}
		private void InitPlayers() {
			player = new Player[2];
			screen.Show("Please entered desired game mode:\n1.PvP\n2.PvE\n3.EvP\n4EvE");
			int mode = keyboard.ReadInt();
			if (mode % 2 == 0) ToggleAiPlayer(1);
			if (mode > 2) ToggleAiPlayer(0);
			for(int i = 0; i < 2; i++)
			{
				if (player[i].IsAI) {
					player[i] = new Player($"AiPlayer{i}");
					InitRandomFleet(player[i],board.size);
				}
				else {
					screen.Show($"Player {i + 1}, please state your name.");
					player[i] = new Player(keyboard.ReadString());
					screen.Show("Do you wish to configure your ships manually(y/n)?");
					if (keyboard.ReadString() != "y") InitRandomFleet(player[i],board.size);
					else {
						for(int j = 0; j < fleet.Count; j++) {
							for(; ; ) {
								screen.Show($"Please enter desired location of ship {j + 1}. Length of ship is {fleet[j]}.");
								Location place = RequestCoords();
								Ship boat = RegisterShip(place, fleet[j]);
								if (ValidateShip(player[i], boat)) { player[i].ships.Add(boat);break; }
								screen.Show("Can't place a ship there in that orientation! Try again.");
							}
							board.PlaceShips(player[i]);
							screen.Clear();
							screen.Show(board.ToString());
						}
						screen.Show($"Fleet configured for {player[i].Name}. Hit enter to continue.");
						keyboard.ReadString();
					}
				}
			}
		}
		private void InitRandomFleet(Player player,int mapSize) {
			Random random = new();
			for (int j = 0; j < fleet.Count; j++) {
				for (; ; ) {
					int x = random.Next(mapSize);
					int y = random.Next(mapSize);
					Location place = new(x, y);
					Ship boat = RegisterShip(place, fleet[j]);
					if (ValidateShip(player,boat)) { player.ships.Add(boat); break; }
				}
			}
		}
		private bool ValidateShip(Player player,Ship boat)
		{
			List<Location> fields = new();
			foreach (Ship ship in player.ships) fields.AddRange(ship.GetShadowMap());
			foreach (Location tile in boat.GetFieldMap()) if (!board.ValidLocation(tile) || board.LocationInList(tile,fields)) return false;
			return true;
		}
		private Location RequestCoords() {
			for(; ; ) { 
				Location place = keyboard.ReadLocation();
				if (board.ValidLocation(place)) return place;
				screen.Show("Location out of bounds! Try again");
			}
		}
		private Ship RegisterShip(Location where,int size) {
			screen.Show("Please choose a direction for this ship:\n1.Up\n2.Right\n3.Down\n4.Left");
			int dir=0;
			while (true) {
				dir = keyboard.ReadInt();
				if (dir > 0 || dir < 5) break;
				screen.Show("Please pick an option between 1 and 4!");
			}
			return new Ship(where, size, dir);
		}
		private int WinMessage(int who) {
			screen.Show($"Congratulation {player[who].Name}. You won.");
			return who;
		}
		private void ToggleAiPlayer(int who) {
			if (who < 0 || who > 1) return;
			player[who].IsAI = !player[who].IsAI;
		}
		public Battleship(Input cin,Display cout) {
			screen = cout;
			keyboard = cin;
		}
		public void Info() {
			string[] readText = File.ReadAllLines("Battleship.inf");
			foreach (string line in readText) screen.Show(line);
		}
		public void Init() {
			screen.Show("Pick board size:");
			board = new(keyboard.ReadInt());
			DefineFleet();
			InitPlayers();
			currentPlayer = 0;
		}
		public void Play() {
			screen.Show($"{player[currentPlayer].Name} pick your move.");
			Location guess = keyboard.ReadLocation();
			int move = 0;
			if(board.ValidLocation(guess)) move=player[currentPlayer].CheckIfHit(guess,player[(currentPlayer+1)%2]);
			switch (move) {
				case 0: { screen.Show("Invalid move. Try again.");break; }
				case 1: { screen.Show("Missed. Go fish!"); break; }
				case 2: { screen.Show("Auch... you hit!"); break; }
				case 3: { screen.Show("Grrr.... You sunk one..."); break; }
			}	
		}
		public int Winner() {
			if (player[0].ships.Count == 0) return WinMessage(1);
			if (player[1].ships.Count == 0) return WinMessage(0);
			return 0;
		}
	}
}