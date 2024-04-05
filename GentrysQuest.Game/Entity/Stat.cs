using System;

namespace GentrysQuest.Game.Entity
{
    public class Stat
    {
        protected string Name; // display name for other languages and etc
        protected StatType StatType; // this is how we get what stat we're looking at

        /// <summary>
        /// This is the bonus stat calculation variable.
        /// Use this in entities UpdateStats method to determine it's effect on the calculation.
        /// </summary>
        public int point;

        public double DefaultValue { get; private set; }
        public double MinimumValue { get; }
        public double CurrentValue { get; private set; }
        public double AdditionalValue { get; private set; }

        public Stat(string name, StatType statType, double minimumValue)
        {
            this.Name = name;
            StatType = statType;
            MinimumValue = minimumValue;
            CurrentValue = Total();
            calculate();
        }

        private void calculate()
        {
            double difference = CurrentValue;
            CurrentValue = Total();
            difference -= CurrentValue;
            UpdateCurrentValue(difference);
        }

        public void RestoreValue()
        {
            CurrentValue = Total();
        }

        public void UpdateCurrentValue(double updateDifference)
        {
            var potentialChange = CurrentValue + updateDifference;

            if (potentialChange > Total()) { CurrentValue = Total(); }
            else if (potentialChange < 0) { CurrentValue = 0; }
            else CurrentValue = potentialChange;
        }

        public void SetDefaultValue(double value)
        {
            DefaultValue = value;
            calculate();
        }

        public void SetAdditionalValue(double value)
        {
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

        public override string ToString()
        {
            return $"{Name}: {MinimumValue + DefaultValue} + {AdditionalValue} ({Total()})";
        }
    }
}
