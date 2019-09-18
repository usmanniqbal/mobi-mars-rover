using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mars_Rover
{
    class Program
    {
        /// <summary>
        /// Regex for landing instructions
        /// </summary>
        static string landingInputFormat = @"^[0-9]{1,7}\s[0-9]{1,7}\s[eEwWnNsS]{1}$";

        /// <summary>
        /// Regex for plateau instructions
        /// </summary>
        static string plateauInputFormat = @"^[0-9]{1,7}\s[0-9]{1,7}$";

        /// <summary>
        /// Regex for navigation instructions
        /// </summary>
        static string navigationInputFormat = @"^[LlrRmM]*$";

        static (string roverName, uint x, uint y, RoverOrientation orientation, char[] navigations)[] commands;

        /// <summary>
        /// Entry point of program
        /// </summary>
        static void Main(string[] args)
        {
            int noOfRovers = Convert.ToInt32(Environment.GetEnvironmentVariable("rovers") ?? "2");
            var plateau = GetPlateau();
            commands = new (string roverName, uint x, uint y, RoverOrientation orientation, char[] navigations)[noOfRovers];

            // Loop for getting information of each rover from user.
            for (int i = 0; i < noOfRovers; i++)
            {
                var roverName = $"Rover {i + 1}";
                var roverLanding = GetRoverLanding(roverName, plateau);
                char[] roverNav = GetNavCommands(roverName);
                commands[i] = (roverName, roverLanding.x, roverLanding.y, roverLanding.orientation, roverNav);
            }

            Console.WriteLine();
            Console.WriteLine("Output");

            // Loop for performing commands associated with each rover.
            foreach (var command in commands)
            {
                var rover = RoverBuilder.Builder(plateau)
                              .SetName(command.roverName)
                              .Landing(command.x, command.y, command.orientation)
                              .Navigate(command.navigations)
                              .Create();

                Console.WriteLine(rover.ToString());
            }
        }

        /// <summary>
        /// Gets input for plateau from user and create its object accordingly.
        /// </summary>
        private static Plateau GetPlateau()
        {
            Console.Write("Plateau: ");
            string input = Console.ReadLine();

            // Checking if the provided input matches the format. If input does not match the pattern it recalls itself to get input again.
            if (!Regex.IsMatch(input, plateauInputFormat))
            {
                Console.WriteLine("Invalid size, input should only be numbers seperated by space.");
                return GetPlateau();
            }

            string[] plateauSize = input.ToUpper().Split(' ');
            uint plateauX = Convert.ToUInt32(plateauSize[0]);
            uint plateauY = Convert.ToUInt32(plateauSize[1]);
            return PlateauBuilder.Builder()
                            .SetX(plateauX)
                            .SetY(plateauY)
                            .Create();
        }

        /// <summary>
        /// Gets navigation commands from user and validates it.
        /// </summary>
        private static char[] GetNavCommands(string roverName)
        {
            Console.Write($"{roverName} Instructions: ");
            string input = Console.ReadLine();

            // Checking if the provided input matches the format. If input does not match the pattern it recalls itself to get input again.
            if (!Regex.IsMatch(input, navigationInputFormat))
            {
                Console.WriteLine("Invalid Commands, navigations instrcution can only have 'L' 'R' or 'M'");
                return GetNavCommands(roverName);
            }
            return input.ToUpper().ToCharArray();
        }

        /// <summary>
        /// Gets landing commands from user and validates it.
        /// </summary>
        private static (uint x, uint y, RoverOrientation orientation) GetRoverLanding(string roverName, Plateau plateau)
        {
            Console.Write($"{roverName} Landing: ");
            string input = Console.ReadLine();

            // Checking if the provided input matches the format. If input does not match the pattern it recalls itself to get input again.
            if (!Regex.IsMatch(input, landingInputFormat))
            {
                Console.WriteLine("Invalid landing commands, input can only be coordinates and orientation e.g. 1 2 E/W/N/S");
                return GetRoverLanding(roverName, plateau);
            }

            string[] roverLanding = input.ToUpper().Split(' ');
            uint roverX = Convert.ToUInt32(roverLanding[0]);
            uint roverY = Convert.ToUInt32(roverLanding[1]);
            Enum.TryParse(roverLanding[2], true, out RoverOrientation roverOrientation);
            string error = string.Empty;

            if (string.IsNullOrEmpty(error))
            {
                return (roverX, roverY, roverOrientation);
            }
            else
            {
                Console.WriteLine(error);
                return GetRoverLanding(roverName, plateau);
            }
        }
    }
}
