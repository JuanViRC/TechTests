using System;
using TechTest;

namespace TechTestTests
{
    internal class MockVehicle : IVehicle
    {
        public string GetDisplayPosition()
        {
            throw new NotImplementedException();
        }

        public void MoveForward()
        {
            throw new NotImplementedException();
        }

        public void TurnLeft()
        {
            throw new NotImplementedException();
        }

        public void TurnRight()
        {
            throw new NotImplementedException();
        }
    }
}