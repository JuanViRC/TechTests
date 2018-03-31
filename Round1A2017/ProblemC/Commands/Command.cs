namespace Round1A2017.ProblemC
{
    public abstract class Command
    {
        protected GameObject RecieverObject { get; set; }

        public Command(GameObject reciever)
        {
            RecieverObject = reciever;
        }

        public abstract void Execute();
    }
}
