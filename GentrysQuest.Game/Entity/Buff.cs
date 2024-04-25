using System;
using GentrysQuest.Game.Utils;

namespace GentrysQuest.Game.Entity
{
    public class Buff
    {
        private double amount;
        private StatType statType;
        private bool isPercent;

        public Buff()
        {
            amount = 1;
            Array values = Enum.GetValues(typeof(StatType));
            Random random = new Random();
            StatType randomType = (StatType)values.GetValue(random.Next(values.Length))!;
            statType = randomType;
            isPercent = MathBase.RandomBool();
        }

        public Buff(double amount, StatType statType, bool isPercent)
        {
            this.amount = amount;
            this.statType = statType;
            this.isPercent = isPercent;
        }
    }
}
