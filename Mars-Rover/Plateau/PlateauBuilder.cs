using System.Collections.Generic;

namespace Mars_Rover
{
    /// <summary>
    /// Builder for Plateau
    /// </summary>

    public class PlateauBuilder
    {
        private readonly Plateau _plateau;
        private PlateauBuilder()
        {
            _plateau = new Plateau();
        }

        public static PlateauBuilder Builder() => new PlateauBuilder();

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