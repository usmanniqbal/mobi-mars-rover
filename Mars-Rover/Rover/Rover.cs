namespace Mars_Rover
{
	/// <summary>
	/// Data holding class for Rover
	/// </summary>
	public abstract class Rover : IRover
	{
		internal Rover()
		{
		}

		public abstract Planet Planet { get; }

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

		public override string ToString() => $"{this.Name} Standing: {this.X} {this.Y} {this.Orientation.ToString()}";

		public static bool operator ==(Rover roverA, Rover roverB)
		{
			return roverA.X == roverB.X && roverA.Y == roverB.Y;
		}
		public static bool operator !=(Rover roverA, Rover roverB)
		{
			return roverA.X != roverB.X || roverA.Y != roverB.Y;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
				return false;
			var roverB = (Rover)obj;
			return this.X == roverB.X && this.Y == roverB.Y;
		}
	}
}