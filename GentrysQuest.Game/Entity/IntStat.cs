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

        public int DefaultValue => (int)Math.Round(base.DefaultValue);

        public int AdditionalValue => (int)Math.Round(base.AdditionalValue);

        public int MinimumValue => (int)Math.Round(base.MinimumValue);

        public int CurrentValue => (int)Math.Round(base.CurrentValue);
    }
}
