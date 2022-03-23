using System.Collections.Generic;

namespace Codecool.Battleship.DataModel {
	public class Location {
		public int x { get; set; }
		public int y { get; set; }
		public Location(int row, int col) {
		}
		public override bool Equals(object l) {
		}

	}
	public class Ship {
		public Location location {get;private set;}
		public int size {get;private set;}
		public int direction {get;private set;}
		public Ship(Location where, int howBig, int heading) {
		}
		public List<Location> ShadowMap() {
		}
		public List<Location> FieldMap();
	}

	public class Player {
		public List<Ship> ships;
		public List<Location> hit, miss, sunk;
		public bool IsAI {get;set;}
		public string Name{ get; set;}
		public Player(string who) { 
		}
		public int CheckIfHit(Location where, Player oponent) { 
		}
		public Location PickHit(int maxSize) { 
		}
	}
}
