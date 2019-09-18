using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mars_Rover.Tests
{
    [TestClass]
    public class MarsRoverTests
    {
        /// <summary>
        /// This method tests positive case where it performs landing and navigation and then validates its result.
        /// </summary>
        [TestMethod]
        public void GetRover_WithNavigation_ReturnsRoverOnNavigatedPosition()
        {
            string roverName = "Rover 1";
            uint landingX = 1;
            uint landingY = 2;
            RoverOrientation orientation = RoverOrientation.N;
            char[] navigations = "LMLMLMLMM".ToCharArray();

            var plateau = GetPlateau();
            var rover = RoverBuilder.Builder(plateau)
                            .SetName(roverName)
                            .Landing(landingX, landingY, orientation)
                            .Navigate(navigations)
                            .Create();

            Assert.AreEqual((uint)1, rover.X);
            Assert.AreEqual((uint)3, rover.Y);
            Assert.AreEqual(RoverOrientation.N, rover.Orientation);
        }

        /// <summary>
        /// This method tests negative case where rover lands outside plateau coordinates.
        /// </summary>
        [TestMethod]
        public void GetRover_WithLandingOutsidePlateauCoordinates_ThrowsIndexOutOfRangeException()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                string roverName = "Rover 1";
                uint landingX = 6;
                uint landingY = 6;
                RoverOrientation orientation = RoverOrientation.N;
                var plateau = GetPlateau();

                RoverBuilder.Builder(plateau)
                               .SetName(roverName)
                               .Landing(landingX, landingY, orientation)
                               .Create();
            });
        }

        /// <summary>
        /// This method tests negative case where rover is navigated in such a way that resultant coordinates are out of plateau's coordinates.
        /// </summary>
        [TestMethod]
        public void GetRover_WithNavigationOutsidePlateauCoordinates_ThrowsIndexOutOfRangeException()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                string roverName = "Rover 1";
                uint landingX = 1;
                uint landingY = 2;
                RoverOrientation orientation = RoverOrientation.N;
                char[] navigations = "LMLMLMLMMMMMM".ToCharArray();
                var plateau = GetPlateau();

                RoverBuilder.Builder(plateau)
                               .SetName(roverName)
                               .Landing(landingX, landingY, orientation)
                               .Navigate(navigations)
                               .Create();
            });
        }

        /// <summary>
        /// This method tests negative case where rover lands on coordinates where there is another rover landed already.
        /// </summary>
        [TestMethod]
        public void GetRover_WithDuplicateLanding_ThrowsIndexOutOfRangeException()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                string rover1Name = "Rover 1";
                string rover2Name = "Rover 2";
                uint landingX = 1;
                uint landingY = 2;
                RoverOrientation orientation = RoverOrientation.N;
                var plateau = GetPlateau();

                RoverBuilder.Builder(plateau)
                               .SetName(rover1Name)
                               .Landing(landingX, landingY, orientation)
                               .Create();

                RoverBuilder.Builder(plateau)
                               .SetName(rover2Name)
                               .Landing(landingX, landingY, orientation)
                               .Create();
            });
        }

        /// <summary>
        /// This method tests negative case where rover is navigated in such a way that resultant coordinates are where there is another rover already.
        /// </summary>
        [TestMethod]
        public void GetRover_WithNavigationOnExistingRoverPosition_ThrowsIndexOutOfRangeException()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                string rover1Name = "Rover 1";
                uint landing1X = 5;
                uint landing1Y = 5;
                string rover2Name = "Rover 2";
                uint landing2X = 4;
                uint landing2Y = 5;
                RoverOrientation orientation = RoverOrientation.N;
                var plateau = GetPlateau();

                RoverBuilder.Builder(plateau)
                               .SetName(rover1Name)
                               .Landing(landing1X, landing1Y, orientation)
                               .Create();

                RoverBuilder.Builder(plateau)
                               .SetName(rover2Name)
                               .Landing(landing2X, landing2Y, orientation)
                               .Navigate('R', 'M')
                               .Create();
            });
        }

        private Plateau GetPlateau()
        {
            uint plateauX = 5;
            uint plateauY = 5;

            return PlateauBuilder.Builder()
                .SetX(plateauX)
                .SetY(plateauY)
                .Create();
        }
    }
}