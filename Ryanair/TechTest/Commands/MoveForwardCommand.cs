using System;
using System.Collections.Generic;
using System.Text;

namespace TechTest.Commands
{
    public class MoveForwardCommand : Command, ICommand
    {
        public MoveForwardCommand(IVehicle vehicle) : base(vehicle)
        {
        }

        public override void Execute()
        {
            this.vehicle.MoveForward();
        }
    }
}
