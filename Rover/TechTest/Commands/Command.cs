using System;
using System.Collections.Generic;
using System.Text;

namespace TechTest.Commands
{
    public abstract class Command : ICommand
    {
        protected readonly IVehicle vehicle;

        public Command(IVehicle vehicle)
        {
            this.vehicle = vehicle;
        }

        public abstract void Execute();
    }
}
