using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTest;

namespace TechTestTests
{
    [TestClass]
    public class RoverTest
    {

        private Position roverInitialPosition;
        private Grid grid;

        [TestInitialize]
        public void Initialize()
        {
            roverInitialPosition = new Position(0, 0);
            grid = new Grid(5, 5);
        }

        [TestMethod]
        public void GivenRoverIsFacingNorth_WhenRiverRotatesLeft_ThenRoverIsFacingWest()
        {
            var rover = new Rover(roverInitialPosition, Direction.North, grid);

            rover.TurnLeft();

            Assert.AreEqual(rover.Direction, Direction.West);
        }

        [TestMethod]
        public void GivenRoverIsFacingWest_WhenRiverRotatesLeft_ThenRoverIsFacingSouth()
        {
            var rover = new Rover(roverInitialPosition, Direction.West, grid);

            rover.TurnLeft();

            Assert.AreEqual(rover.Direction, Direction.South);
        }

        [TestMethod]
        public void GivenRoverIsFacingSouth_WhenRiverRotatesLeft_ThenRoverIsFacingEast()
        {
            var rover = new Rover(roverInitialPosition, Direction.South, grid);

            rover.TurnLeft();

            Assert.AreEqual(rover.Direction, Direction.East);
        }

        [TestMethod]
        public void GivenRoverIsFacingEast_WhenRiverRotatesLeft_ThenRoverIsFacingNorth()
        {
            var rover = new Rover(roverInitialPosition, Direction.East, grid);

            rover.TurnLeft();

            Assert.AreEqual(rover.Direction, Direction.North);
        }



        [TestMethod]
        public void GivenRoverIsFacingNorth_WhenRiverRotatesRight_ThenRoverIsFacingEast()
        {
            var rover = new Rover(roverInitialPosition, Direction.North, grid);

            rover.TurnRight();

            Assert.AreEqual(rover.Direction, Direction.East);
        }

        [TestMethod]
        public void GivenRoverIsFacingEast_WhenRiverRotatesRight_ThenRoverIsFacingSouth()
        {
            var rover = new Rover(roverInitialPosition, Direction.East, grid);

            rover.TurnRight();

            Assert.AreEqual(rover.Direction, Direction.South);
        }

        [TestMethod]
        public void GivenRoverIsFacingSouth_WhenRiverRotatesRight_ThenRoverIsFacingWest()
        {
            var rover = new Rover(roverInitialPosition, Direction.South, grid);

            rover.TurnRight();

            Assert.AreEqual(rover.Direction, Direction.West);
        }

        [TestMethod]
        public void GivenRoverIsFacingWest_WhenRiverRotatesRight_ThenRoverIsFacingNorth()
        {
            var rover = new Rover(roverInitialPosition, Direction.West, grid);

            rover.TurnRight();

            Assert.AreEqual(rover.Direction, Direction.North);
        }



        [TestMethod]
        public void GivenRoverIsAtPosition_1_1_AndRoverIsFacingNorth_WhenRoverMovesForward_ThenRoverIsInPosition_0_1()
        {
            roverInitialPosition = new Position(1, 1);
            var rover = new Rover(roverInitialPosition, Direction.North, grid);

            rover.MoveForward();

            Assert.AreEqual(0, rover.Position.X);
            Assert.AreEqual(1, rover.Position.Y, 1);
        }

        [TestMethod]
        public void GivenRoverIsAtPosition_1_1_AndRoverIsFacingWest_WhenRoverMovesForward_ThenRoverIsInPosition_1_0()
        {
            roverInitialPosition = new Position(1, 1);
            var rover = new Rover(roverInitialPosition, Direction.West, grid);

            rover.MoveForward();

            Assert.AreEqual(1, rover.Position.X);
            Assert.AreEqual(0, rover.Position.Y);
        }

        [TestMethod]
        public void GivenRoverIsAtPosition_1_1_AndRoverIsFacingSouth_WhenRoverMovesForward_ThenRoverIsInPosition_2_1()
        {
            roverInitialPosition = new Position(1, 1);
            var rover = new Rover(roverInitialPosition, Direction.South, grid);

            rover.MoveForward();

            Assert.AreEqual(2, rover.Position.X);
            Assert.AreEqual(1, rover.Position.Y);
        }

        [TestMethod]
        public void GivenRoverIsAtPosition_1_1_AndRoverIsFacingEast_WhenRoverMovesForward_ThenRoverIsInPosition_1_2()
        {
            roverInitialPosition = new Position(1, 1);
            var rover = new Rover(roverInitialPosition, Direction.East, grid);

            rover.MoveForward();

            Assert.AreEqual(1, rover.Position.X);
            Assert.AreEqual(2, rover.Position.Y);
        }


        [TestMethod]
        public void WhenUserMovesRoverTo_1_1_ThenRoverPositionIsDisplayedInCorrectFormat()
        {
            roverInitialPosition = new Position(1, 1);
            var rover = new Rover(roverInitialPosition, Direction.East, grid);

            var currentDisplayedPosition = rover.GetDisplayPosition();

            Assert.AreEqual("(1,1)", currentDisplayedPosition);
        }

        [TestMethod]
        public void GivenRoverIsFacingWestAndPosition_0_0_WhenUserMovesForward_ThenRoversPositionDoesNotChange()
        {
            roverInitialPosition = new Position(0, 0);
            var rover = new Rover(roverInitialPosition, Direction.West, grid);

            rover.MoveForward();

            Assert.AreEqual(0, rover.Position.X);
            Assert.AreEqual(0, rover.Position.Y);
        }

    }
}
