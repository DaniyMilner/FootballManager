namespace DomainModel.Entities
{
    public class Illness : Entity
    {
        protected internal Illness() { }

        protected internal Illness(string illnessName, int timeForRecovery)
        {
            IllnessName = illnessName;
            TimeForRecovery = timeForRecovery;
        }

        public string IllnessName { get; private set; }
        public int TimeForRecovery { get; private set; }
    }
}
