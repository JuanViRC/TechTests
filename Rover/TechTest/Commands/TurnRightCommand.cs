using System;
using System.Collections.Generic;
using System.Text;

namespace TechTest.Commands
{
    public class TurnRightCommand : Command, ICommand
    {
        public TurnRightCommand(IVehicle vehicle) : base(vehicle)
        {
        }

        public override void Execute()
        {
            this.vehicle.TurnRight();
        }
    }
}
