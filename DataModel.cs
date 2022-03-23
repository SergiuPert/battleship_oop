using System.Collections.Generic;
using System;

namespace Codecool.Battleship.DataModel {
	public class Location
	{

		public int x { get; set; }
		public int y { get; set; }
		public Location(int row, int col) 
		{
			x = row;	
			y = col;
		}
        public override bool Equals(Object location)
        {
            Location location1 = (Location)location;
			if ((location1.x == this.x) && (location1.y == this.y))
				return true;
			else 
				return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
	public class Ship {
		public Location location {get;private set;}
		public int size {get;private set;}
		public int direction {get;private set;}
		public Ship(Location where, int howBig, int heading)
		{
			location = where;
			size = howBig;
			direction = heading;
		}

		public List<Location> GetShadowMap() 
		
		{
			List <Location> locationList = new List<Location>();
			
			
			return locationList;
		}
		public List<Location> GetFieldMap()
		{ 
			
		}

		// coordonatele shipului
	}

	public enum ShipDirection
	{ 
		None = 0,
		Up = 1,
		Right = 2,
		Down = 3,
		Left = 4,
	}
	public class Player {
		public List<Ship> ships;
		public List<Location> hits, misses, sunks;
		public bool IsAI;
		public string Name;
		public Player(string who) { 
			Name = who;
		}
		public int CheckIfHit(Location where, Player oponent) { 
		}
		public Location PickHit(int maxSize) { 
		}
	}
}
