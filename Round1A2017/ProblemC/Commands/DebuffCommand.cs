namespace Round1A2017.ProblemC
{
    public class DebuffCommand : Command
    {
        private readonly int debuffPower;

        public DebuffCommand(GameObject reciever, int debuffPower) : base(reciever)
        {
            this.debuffPower = debuffPower;
        }

        public override void Execute()
        {
            RecieverObject.Debuffed(debuffPower);
        }
    }
}
