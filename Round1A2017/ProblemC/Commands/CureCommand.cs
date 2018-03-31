namespace Round1A2017.ProblemC
{
    public class CureCommand : Command
    {
        public CureCommand(GameObject reciever) : base(reciever) { }

        public override void Execute()
        {
            RecieverObject.Cured();
        }
    }
}
