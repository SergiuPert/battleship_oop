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
			switch (direction)
			{
				case (int)ShipDirection.Up:
					{
						for (int i = 0; i < size; i++)
						{
							Location placeShip = new Location(location.x, location.y - i);
							Location placeShip1 = new Location(location.x+1, location.y - i);
							Location placeShip2 = new Location(location.x-1, location.y - i);
							locationList.Add(placeShip);
							locationList.Add(placeShip1);
							locationList.Add(placeShip2);
.					    }
						Location placeShipDown = new Location(location.x, location.y+1);
						Location placeShipUp = new Location(location.x, location.y - size);
						locationList.Add(placeShipDown);
						locationList.Add(placeShipUp);
						break;
					}
				case (int)ShipDirection.Down:
					{
						for (int i = 0; i < size; i++)
						{
							Location placeShip = new Location(location.x, location.y + i);
							Location placeShip1 = new Location(location.x + 1, location.y + i);
							Location placeShip2 = new Location(location.x - 1, location.y + i);
							locationList.Add(placeShip);
							locationList.Add(placeShip1);
							locationList.Add(placeShip2);
.					    }
						Location placeShipDown = new Location(location.x, location.y - 1);
						Location placeShipUp = new Location(location.x, location.y + size);
						locationList.Add(placeShipDown);
						locationList.Add(placeShipUp);
						break;
					}
				case (int)ShipDirection.Left:
					{
						for (int i = 0; i < size; i++)
						{
							Location placeShip = new Location(location.x - i, location.y);
							Location placeShip1 = new Location(location.x - i, location.y + 1);
							Location placeShip2 = new Location(location.x - i, location.y - 1);
							locationList.Add(placeShip);
							locationList.Add(placeShip1);
							locationList.Add(placeShip2);
.					    }
						Location placeShipLeft = new Location(location.x + 1, location.y);
						Location placeShipRight = new Location(location.x - size, location.y);
						locationList.Add(placeShipLeft);
						locationList.Add(placeShipRight);
						break;
					}
				case (int)ShipDirection.Right:
					{
						for (int i = 0; i < size; i++)
						{
							Location placeShip = new Location(location.x + i, location.y);
							Location placeShip1 = new Location(location.x + i, location.y - 1);
							Location placeShip2 = new Location(location.x + i, location.y + 1);
							locationList.Add(placeShip);
							locationList.Add(placeShip1);
							locationList.Add(placeShip2);
.					    }
						Location placeShipLeft = new Location(location.x - 1, location.y);
						Location placeShipRight = new Location(location.x + size, location.y);
						locationList.Add(placeShipLeft);
						locationList.Add(placeShipRight);
						break;
					}

			}

			return locationList;
		}
		public List<Location> GetFieldMap()
		{ 
			List<Location> locations = new List<Location>();

			switch(direction)
			{
				case (int)ShipDirection.Up:
					{
						for (int i = 0; i < size; i++)
						{
							Location placeShip = new Location(location.x, location.y - i);
							locations.Add(placeShip);
.					    }
						break;
					}
				case (int)ShipDirection.Down:
					{
						for (int i = 0; i < size; i++)
						{
							Location placeShip = new Location(location.x, location.y + i);
							locations.Add(placeShip);
						}
						break;
					}
				case (int)ShipDirection.Left:
					{
						for (int i = 0; i < size; i++)
						{
							Location placeShip = new Location(location.x - i, location.y);
							locations.Add(placeShip);
						}
						break;
					}
				case (int)ShipDirection.Right:
					{
						for (int i = 0; i < size; i++)
						{
							Location placeShip = new Location(location.x + i, location.y);
							locations.Add(placeShip);
						}
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
		public int CheckIfHit(Location where, Player oponent) { 
		}
		public Location PickHit(int maxSize) { 
		}
	}
}
