﻿using System;

namespace TechTest
{
    public class RoverManager
    {

        public static void Main()
        {
            var position = new Position(0, 0);
            var grid = new Grid(5, 5);
            var rover = new Rover(position, Direction.North, grid);

            var roverCommandFactory = new VehicleCommandFactory(rover);

            var driver = new Driver();

            Console.WriteLine(rover.GetDisplayPosition());
            
            while (true)
            {
                var stringCommand = Console.ReadLine();
                var command = roverCommandFactory.CreateCommand(stringCommand);

                driver.SetCommand(command);
                driver.Drive();

                Console.WriteLine(rover.GetDisplayPosition());
            }
        }

        /*
        public static void Main()
        {
            var roverPositionX = 0;
            var roverPositionY = 0;
            var roverFacing = RoverFacing.North;

            while (true)
            {
                var command = Console.ReadLine();
                if (command != "L" && command != "R" && command != "F")
                    throw new Exception("invalid command");

                switch (command)
                {
                    case "L":
                        roverFacing = roverFacing == RoverFacing.North ? RoverFacing.West : (RoverFacing)((int)roverFacing - 1);
                        Console.WriteLine($"Rover is now at {roverPositionX}, {roverPositionY} - facing {roverFacing}");
                        break;
                    case "R":
                        roverFacing = roverFacing == RoverFacing.West ? RoverFacing.North : (RoverFacing)((int)roverFacing + 1);
                        Console.WriteLine($"Rover is now at {roverPositionX}, {roverPositionY} - facing {roverFacing}");
                        break;
                    case "F":
                        switch (roverFacing)
                        {
                            case RoverFacing.North:
                                roverPositionX++;
                                break;
                            case RoverFacing.East:
                                roverPositionY++;
                                break;
                            case RoverFacing.South:
                                roverPositionX--;
                                break;
                            case RoverFacing.West:
                                roverPositionY--;
                                break;
                        }
                        Console.WriteLine($"Rover is now at {roverPositionX}, {roverPositionY} - facing {roverFacing}");
                        break;
                    default:
                        throw new Exception("invalid command");
                }
            }
        }
        */
    }
}