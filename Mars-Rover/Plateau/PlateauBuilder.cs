using System.Collections.Generic;

namespace Mars_Rover
{
	/// <summary>
	/// Builder for Plateau
	/// </summary>

	public class PlateauBuilder
	{
		private readonly Plateau _plateau;
		private PlateauBuilder(Planet planet)
		{
			switch (planet)
			{
				case Planet.Mars:
					_plateau = new MarsPlateau();
					break;
				case Planet.Venus:
					_plateau = new VenusPlateau();
					break;
			}
		}

		public static PlateauBuilder Builder(Planet planet) => new PlateauBuilder(planet);

		/// <summary>
		/// Sets maximum x-axis value of plateau
		/// </summary>
		public PlateauBuilder SetX(uint x)
		{
			_plateau.X = x;
			return this;
		}

		/// <summary>
		/// Sets maximum y-axis value of plateau
		/// </summary>
		public PlateauBuilder SetY(uint y)
		{
			_plateau.Y = y;
			return this;
		}

		public Plateau Create()
		{
			return _plateau;
		}
	}
}