using System;

namespace GentrysQuest.Game.Entity
{
    public class IntStat : Stat
    {
        public IntStat(string name, StatType statType, int minimumValue, bool resetsOnUpdate = true)
            : base(name, statType, minimumValue, resetsOnUpdate)
        {
            // The IntStat constructor.
        }

        public int DefaultValue => (int)Math.Round(base.DefaultValue);

        public int AdditionalValue => (int)Math.Round(base.AdditionalValue);

        public int MinimumValue => (int)Math.Round(base.MinimumValue);

        public int CurrentValue => (int)Math.Round(base.CurrentValue);
    }
}
