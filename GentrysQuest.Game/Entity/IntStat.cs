using System;

namespace GentrysQuest.Game.Entity
{
    public class IntStat : Stat
    {
        public IntStat(string name, StatTypes statType, int minimumValue)
            : base(name, statType, minimumValue)
        {
            // The IntStat constructor.
        }

        public int DefaultValue => (int)Math.Round(defaultValue);

        public int AdditionalValue => (int)Math.Round(additionalValue);

        public int MinimumValue => (int)Math.Round(minimumValue);

        public int CurrentValue => (int)Math.Round(currentValue);
    }
}
