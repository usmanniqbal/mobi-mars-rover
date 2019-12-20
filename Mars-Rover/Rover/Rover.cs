namespace Mars_Rover
{
	/// <summary>
	/// Data holding class for Rover
	/// </summary>
	public class Rover
	{
		internal Rover()
		{
		}

		/// <summary>
		/// Name of rover.
		/// </summary>
		public string Name { get; internal set; }

		/// <summary>
		/// Current position of rover on x-axis.
		/// </summary>
		public uint X { get; internal set; }

		/// <summary>
		/// Current position of rover on y-axis.
		/// </summary>
		public uint Y { get; internal set; }

		/// <summary>
		/// Current orientation of rover to which its heading.
		/// </summary>
		public RoverOrientation Orientation { get; internal set; }

		public override string ToString() => $"{this.Name}: {this.X} {this.Y} {this.Orientation.ToString()}";
	}
}