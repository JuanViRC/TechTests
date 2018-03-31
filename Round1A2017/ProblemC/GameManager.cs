using System.Collections.Generic;

namespace Round1A2017.ProblemC
{
    public class GameManager
    {
        public ICollection<Command> HistoryCommands { get; set; }
        public ICollection<Command> TurnCommands { get; set; }
        private readonly GameObject dragon;
        private readonly GameObject knight;

        public GameManager(GameObject dragon, GameObject knight)
        {
            TurnCommands = new List<Command>();
            HistoryCommands = new List<Command>();
            this.dragon = dragon;
            this.knight = knight;
        }

        public void AddCommand(Command command)
        {
            TurnCommands.Add(command);
        }

        public void PlayTurn()
        {
            foreach (var command in TurnCommands)
            {
                command.Execute();
                HistoryCommands.Add(command);

                if (knight.IsDead()) throw new GameEndWin();
                if (dragon.IsDead()) throw new GameEndFaild();
            }

            TurnCommands.Clear();
        }

    }
}
