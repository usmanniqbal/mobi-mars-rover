using System;
using System.Linq;

namespace Mars_Rover
{
	public class RoverBuilder
	{
		private readonly Rover _rover;
		private readonly Plateau _plateau;

		private RoverBuilder(Plateau plateau)
		{
			switch (plateau.Planet)
			{
				case Planet.Mars:
					_rover = new MarsRover();
					break;
				case Planet.Venus:
					_rover = new VenueRover();
					break;
			}
			_plateau = plateau;
		}

		private RoverBuilder(Planet planet, uint plateauX, uint plateauY)
		{
			var plateau = PlateauBuilder.Builder(planet)
							.SetX(plateauX)
							.SetY(plateauY)
							.Create();

			switch (plateau.Planet)
			{
				case Planet.Mars:
					_rover = new MarsRover();
					break;
				case Planet.Venus:
					_rover = new VenueRover();
					break;
			}
			_plateau = plateau;
		}

		public static RoverBuilder Builder(Plateau plateau) => new RoverBuilder(plateau);
		public static RoverBuilder Builder(Planet planet, uint plateauX, uint plateauY) => new RoverBuilder(planet, plateauX, plateauY);

		/// <summary>
		/// This method sets the name of rover. 
		/// </summary>
		public RoverBuilder SetName(string name)
		{
			_rover.Name = name;
			return this;
		}

		/// <summary>
		/// This method lands rover on specified position on the plateau. 
		/// </summary>
		public RoverBuilder Landing(uint x, uint y, RoverOrientation orientation)
		{
			_rover.X = x;
			_rover.Y = y;

			if (x > _plateau.X || y > _plateau.Y)
			{
				throw new IndexOutOfRangeException($"{_rover.Name}'s landing coordinates can not be outside Plateau's coordinates");
			}
			else if (_plateau.Rovers.Contains(_rover))
			{
				throw new IndexOutOfRangeException($"{_rover.Name} can not land on {x},{y} as there is already a rover on the provided coordinates");
			}

			_rover.Orientation = orientation;
			_plateau.Rovers.Add(_rover);
			return this;
		}

		/// <summary>
		/// Navigates the rover on plateau according to the provided navigation instruction, possible values are ('L', 'R' and 'M').
		/// </summary>
		public RoverBuilder Navigate(params char[] navigations)
		{
			for (int i = 0; i < navigations.Length; i++)
			{
				switch (navigations[i])
				{
					case 'L':
						_rover.Orientation = _rover.Orientation == RoverOrientation.S ? RoverOrientation.E : _rover.Orientation + 1;
						break;
					case 'R':
						_rover.Orientation = _rover.Orientation == RoverOrientation.E ? RoverOrientation.S : _rover.Orientation - 1;
						break;
					case 'M':
						int x = (int)_rover.X;
						int y = (int)_rover.Y;

						// Rover Moves forward on x-axis if its headed already towards East.
						x += _rover.Orientation == RoverOrientation.E ? 1 : 0;

						// Rover Moves backward on x-axis if its headed already towards West.
						x -= _rover.Orientation == RoverOrientation.W ? 1 : 0;

						// Rover Moves upward on y-axis if its headed already towards North.
						y += _rover.Orientation == RoverOrientation.N ? 1 : 0;

						// Rover Moves downward on y-axis if its headed already towards South.
						y -= _rover.Orientation == RoverOrientation.S ? 1 : 0;

						if ((x < 0 || x > _plateau.X) || (y < 0 || y > _plateau.Y))
						{
							throw new IndexOutOfRangeException($"{_rover.Name} can not navigate outside Plateau's coordinates");
						}

						_rover.X = (uint)x;
						_rover.Y = (uint)y;
						break;
					default:
						break;
				}
			}

			if (_plateau.Rovers.Any(o => o.Name != _rover.Name && o.X == _rover.X && o.Y == _rover.Y))
			{
				throw new IndexOutOfRangeException($"{_rover.Name} can not navigate to {_rover.X},{_rover.Y} as there is already a rover.");
			}

			return this;
		}

		public Rover Create()
		{
			return _rover;
		}
	}
}