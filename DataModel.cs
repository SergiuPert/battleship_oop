using System.Collections.Generic;
using System;
using System.Linq;

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
			if ((location1.x == x) && (location1.y == y))
				return true;
			else 
				return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

		public bool LocationInList(List<Location> locations)
		{
			foreach (Location target in locations)
			{
				if (target.x == x && target.y == y)
				{
					return true;
				}
			}
			return false;
		}

		public Location GetListedLocation(List<Location> locations)
		{
			foreach (Location target in locations)
			{
				if (target.x == x && target.y == y)
				{
					return target;
				}
			}
			return null;
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
			List <Location> locationList = new();
			switch (direction)
			{
				case (int)ShipDirection.Up:
					{
						for (int i = 0; i < size; i++)
						{
							locationList.Add(new Location(location.x, location.y - i));
							locationList.Add(new Location(location.x+1, location.y - i));
							locationList.Add(new Location(location.x - 1, location.y - i));
						}
						locationList.Add(new Location(location.x, location.y+1));
						locationList.Add(new Location(location.x, location.y - size));
						break;
					}
				case (int)ShipDirection.Down:
					{
						for (int i = 0; i < size; i++)
						{
							locationList.Add(new Location(location.x, location.y + i));
							locationList.Add(new Location(location.x + 1, location.y + i));
							locationList.Add(new Location(location.x - 1, location.y + i));
					    }
						locationList.Add(new Location(location.x, location.y - 1));
						locationList.Add(new Location(location.x, location.y + size));
						break;
					}
				case (int)ShipDirection.Left:
					{
						for (int i = 0; i < size; i++)
						{
							locationList.Add(new Location(location.x - i, location.y));
							locationList.Add(new Location(location.x - i, location.y + 1));
							locationList.Add(new Location(location.x - i, location.y - 1));
					    }
						locationList.Add(new Location(location.x + 1, location.y));
						locationList.Add(new Location(location.x - size, location.y));
						break;
					}
				case (int)ShipDirection.Right:
					{
						for (int i = 0; i < size; i++)
						{
							locationList.Add(new Location(location.x + i, location.y));
							locationList.Add(new Location(location.x + i, location.y - 1));
							locationList.Add(new Location(location.x + i, location.y + 1));
					    }
						locationList.Add(new Location(location.x - 1, location.y));
						locationList.Add(new Location(location.x + size, location.y));
						break;
					}
			}
			return locationList;
		}
		public List<Location> GetFieldMap()
		{ 
			List<Location> locations = new();
			switch(direction)
			{
				case (int)ShipDirection.Up:
					{
						for (int i = 0; i < size; i++) locations.Add(new Location(location.x, location.y - i));
						break;
					}
				case (int)ShipDirection.Down:
					{
						for (int i = 0; i < size; i++) locations.Add(new Location(location.x, location.y + i));
						break;
					}
				case (int)ShipDirection.Left:
					{
						for (int i = 0; i < size; i++) locations.Add(new Location(location.x - i, location.y));
						break;
					}
				case (int)ShipDirection.Right:
					{
						for (int i = 0; i < size; i++) locations.Add(new Location(location.x + i, location.y));
						break;
					}
			}
			return locations;
		}
	}

    public enum ShipDirection
    {
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
		public int CheckIfHit(Location where, Player oponent) 
		{
			foreach(Ship ship in oponent.ships)
			{
				List<Location> ShipSquares = ship.GetFieldMap();
				foreach (Location location in ShipSquares) // fiecare locatie din shipul curent(iteratia respectiva)
				{
					if (location.Equals(where))
					{ 
					hits.Add(location);
					bool isSunk = true;
						foreach (Location locationPlace in ShipSquares) // vorbim de acelasi ship de mai sus, luam patratele shipului
						{
							if (!locationPlace.LocationInList(hits)) isSunk = false;
						}
						if (isSunk)
						{
							foreach (Location locationPlace1 in ShipSquares) hits.Remove(locationPlace1.GetListedLocation(hits));
							sunks.AddRange(ShipSquares);
							oponent.ships.Remove(ship);
							return 3;
						}
						else return 2;
					}
				}
			}
			misses.Add(where);
			return 1;
		}
		public Location PickHit(int maxSize) 
		{ 
			Random random = new(maxSize);
			while (true)
			{
				Location location = new(random.Next(), random.Next());
				if (!location.LocationInList(hits)&&!location.LocationInList(misses)&&!location.LocationInList(sunks)) return location;
			}
		}
	}
}
