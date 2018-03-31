namespace Round1A2017.ProblemC
{
    public class AttackCommand : Command
    {
        private readonly int attackPower;

        public AttackCommand(GameObject reciever, int attackPower) : base(reciever)
        {
            this.attackPower = attackPower;
        }

        public override void Execute()
        {
            RecieverObject.Attacked(attackPower);
        }
    }
}
