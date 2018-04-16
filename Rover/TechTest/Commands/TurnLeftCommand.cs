using System;
using System.Collections.Generic;
using System.Text;

namespace TechTest.Commands
{
    public class TurnLeftCommand : Command, ICommand
    {
        public TurnLeftCommand(IVehicle vehicle) : base(vehicle)
        {
        }

        public override void Execute()
        {
            this.vehicle.TurnLeft();
        }
    }
}
