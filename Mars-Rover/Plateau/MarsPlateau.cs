using System;
using System.Collections.Generic;
using System.Text;

namespace Mars_Rover
{
	public class VenusPlateau : Plateau
	{
		public override Planet Planet => Planet.Venus;
	}
}
