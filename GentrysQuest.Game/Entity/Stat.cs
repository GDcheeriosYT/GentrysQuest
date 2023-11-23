namespace GentrysQuest.Game.Entity
{
    public class Stat
    {
        protected string name; // display name for other languages and etc
        protected StatTypes statType; // this is how we get what stat we're looking at
        protected int point; // this determines bonus stat value for entity
        protected double defaultValue { get; private set; }
        private double oldTotal;
        protected double minimumValue { get; }
        protected double currentValue { get; private set; }
        protected double additionalValue { get; private set; }

        public Stat(string name, StatTypes statType, double minimumValue)
        {
            this.name = name;
            this.statType = statType;
            this.minimumValue = minimumValue;
        }

        private void Calculate()
        {
            UpdateCurrentValue(oldTotal - Total());
        }

        public void UpdateCurrentValue(double updateDifference)
        {
            currentValue += updateDifference;
        }

        public void SetDefaultValue(double value)
        {
            oldTotal = Total();
            this.defaultValue = value;
            Calculate();
        }

        public void SetAdditionalValue(double value)
        {
            oldTotal = Total();
            this.additionalValue = value;
        }

        protected Stat()
        {
            throw new System.NotImplementedException();
        }

        public double Total()
        {
            return minimumValue + defaultValue + additionalValue;
        }

        public double Difference()
        {
            return Total() - currentValue;
        }
    }
}
