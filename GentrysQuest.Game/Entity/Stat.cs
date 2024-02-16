using System;
using osu.Framework.Logging;

namespace GentrysQuest.Game.Entity
{
    public class Stat
    {
        protected string name; // display name for other languages and etc
        protected StatTypes statType; // this is how we get what stat we're looking at
        protected int point; // this determines bonus stat value for entity
        public double DefaultValue { get; private set; }
        private double oldTotal;
        public double MinimumValue { get; }
        public double CurrentValue { get; private set; }
        public double AdditionalValue { get; private set; }

        public Stat(string name, StatTypes statType, double minimumValue)
        {
            this.name = name;
            this.statType = statType;
            MinimumValue = minimumValue;
        }

        private void calculate()
        {
            UpdateCurrentValue(oldTotal - Total());
        }

        public void UpdateCurrentValue(double updateDifference)
        {
            Logger.Log("updateDiff: " + updateDifference);
            CurrentValue += updateDifference;
        }

        public void SetDefaultValue(double value)
        {
            oldTotal = Total();
            DefaultValue = value;
            calculate();
        }

        public void SetAdditionalValue(double value)
        {
            oldTotal = Total();
            AdditionalValue = value;
            calculate();
        }

        protected Stat()
        {
            throw new NotImplementedException();
        }

        public double Total()
        {
            return MinimumValue + DefaultValue + AdditionalValue;
        }

        public double Difference()
        {
            return Total() - CurrentValue;
        }
    }
}
