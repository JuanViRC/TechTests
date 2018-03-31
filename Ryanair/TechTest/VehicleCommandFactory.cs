using System;
using System.Collections.Generic;
using System.Text;
using TechTest.Commands;

namespace TechTest
{
    public class VehicleCommandFactory
    {
        private IVehicle vehicle;

        public VehicleCommandFactory(IVehicle vehicle)
        {
            this.vehicle = vehicle;
        }

        public ICommand CreateCommand(string command)
        {
            switch (command)
            {
                case "R":
                    return new TurnRightCommand(this.vehicle);
                case "L":
                    return new TurnLeftCommand(this.vehicle);
                case "F":
                    return new MoveForwardCommand(this.vehicle);
                default:
                    throw new ArgumentException("Valid commands are R, L or F");
            }
        }
    }
}
