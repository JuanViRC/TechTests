namespace Round1A2017.ProblemC
{
    public class GameObject
    {
        private GameObject Enemy { get; set; }
        private int MaxHealth { get; set; }
        public int Health { get; private set; }
        public int AttackPower { get; private set; }

        public GameObject(int health, int attack)
        {
            MaxHealth = health;
            Health = health;
            AttackPower = attack;
        }

        public void SetEnemy(GameObject enemy)
        {
            Enemy = enemy;
        }

        public void Attacked(int attackPower)
        {
            Health -= attackPower;
            if (Health < 0) Health = 0;
        }

        public void Buffed(int buffPower)
        {
            AttackPower += buffPower;
        }

        public void Cured()
        {
            Health = MaxHealth;
        }

        public void Debuffed(int debuffPorwer)
        {
            AttackPower -= debuffPorwer;
            if (AttackPower < 0) AttackPower = 0;
        }

        public AttackCommand Attack()
        {
            return new AttackCommand(Enemy, AttackPower);
        }

        public CureCommand Cure()
        {
            return new CureCommand(this);
        }

        public BuffCommand Buff(int buffStat)
        {
            return new BuffCommand(this, buffStat);
        }

        public DebuffCommand Debuff(int debuffStat)
        {
            return new DebuffCommand(Enemy, debuffStat);
        }

        public bool IsDead()
        {
            return Health < 1;
        }
    }
}
