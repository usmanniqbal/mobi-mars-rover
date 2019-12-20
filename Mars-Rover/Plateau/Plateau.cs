using System.Collections.Generic;

namespace Mars_Rover
{
	/// <summary>
	/// Data holding class for Plateau
	/// </summary>

	public abstract class Plateau
	{
		internal Plateau()
		{
		}

		public abstract Planet Planet { get; }

		/// <summary>
		/// X coordinate of plateau specify the width.
		/// </summary>
		public uint X { get; set; }

		/// <summary>
		/// Y coordinate of plateau specify the height.
		/// </summary>
		public uint Y { get; set; }

		/// <summary>
		/// Rovers onboard.
		/// </summary>
		public IList<Rover> Rovers { get; } = new List<Rover>();
	}
}