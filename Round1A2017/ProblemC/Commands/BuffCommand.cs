namespace Round1A2017.ProblemC
{
    public class BuffCommand : Command
    {
        private readonly int buffPower;

        public BuffCommand(GameObject reciever, int buffPower) : base(reciever)
        {
            this.buffPower = buffPower;
        }

        public override void Execute()
        {
            RecieverObject.Buffed(buffPower);
        }
    }
}
