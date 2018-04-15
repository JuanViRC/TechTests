using System;
using System.Collections.Generic;
using System.Text;
using TechTest.Commands;

namespace TechTest
{
    public class Driver
    {
        private ICommand command;

        public void SetCommand(ICommand command)
        {
            this.command = command;
        }

        public void Drive()
        {
            if (this.command == null) throw new ArgumentNullException();
            this.command.Execute();            
        }

    }
}
