using System;
using System.Collections.Generic;
using System.Text;

namespace Mars_Rover
{
	public class VenueRover : Rover
	{
		public override Planet Planet => Planet.Venus;
	}
}
