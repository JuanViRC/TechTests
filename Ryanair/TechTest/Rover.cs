using System;
using System.Collections.Generic;
using System.Text;

namespace TechTest
{
    public class Rover : IVehicle
    {
        public IGrid Grid { get; private set; }

        public IPosition Position { get; private set; }

        public Direction Direction { get; private set; }

        public Rover(IPosition initialPosition, Direction initialDirection, IGrid grid)
        {
            Direction = initialDirection;
            Position = initialPosition ?? throw new ArgumentNullException();
            Grid = grid ?? throw new ArgumentException();
        }

        public void TurnLeft()
        {
            if (Direction == Direction.North)
            {
                Direction = Direction.West;
            }
            else if (Direction == Direction.West)
            {
                Direction = Direction.South;
            }
            else if (Direction == Direction.South)
            {
                Direction = Direction.East;
            }
            else if (Direction == Direction.East)
            {
                Direction = Direction.North;
            }
        }

        public void TurnRight()
        {
            if (Direction == Direction.North)
            {
                Direction = Direction.East;
            }
            else if (Direction == Direction.West)
            {
                Direction = Direction.North;
            }
            else if (Direction == Direction.South)
            {
                Direction = Direction.West;
            }
            else if (Direction == Direction.East)
            {
                Direction = Direction.South;
            }
        }

        public void MoveForward()
        {
            if (Direction == Direction.North)
            {
                if (Position.X == 0) return;
                Position = new Position(Position.X - 1, Position.Y);
            }
            else if (Direction == Direction.West)
            {
                if (Position.Y == 0) return;
                Position = new Position(Position.X, Position.Y - 1);
            }
            else if (Direction == Direction.South)
            {
                if (Position.X == Grid.LengthX) return;
                Position = new Position(Position.X + 1, Position.Y);
            }
            else if (Direction == Direction.East)
            {
                if (Position.X == Grid.LengthY) return;
                Position = new Position(Position.X, Position.Y + 1);
            }
        }

        public string GetDisplayPosition()
        {
            return $"({Position.X},{Position.Y})";
        }

    }
}
