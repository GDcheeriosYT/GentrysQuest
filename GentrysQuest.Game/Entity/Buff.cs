using System;
using GentrysQuest.Game.Utils;
using osu.Framework.Bindables;

namespace GentrysQuest.Game.Entity
{
    public class Buff
    {
        public StatType StatType { get; private set; }
        public bool IsPercent { get; private set; }
        public int Level { get; private set; } = 1;
        public Bindable<double> Value { get; private set; } = new Bindable<double>(0);
        public EntityBase ParentEntity;

        public Buff(EntityBase parentEntity)
        {
            ParentEntity = parentEntity;
            StatType = GetRandomStat();
            IsPercent = MathBase.RandomBool();
            handlePostConstruction();
        }

        public Buff(EntityBase parentEntity, StatType statType, bool isPercent)
        {
            ParentEntity = parentEntity;
            StatType = statType;
            IsPercent = isPercent;
            handlePostConstruction();
        }

        public Buff(EntityBase parentEntity, StatType statType)
        {
            ParentEntity = parentEntity;
            StatType = statType;
            IsPercent = MathBase.RandomBool();
            handlePostConstruction();
        }

        public Buff(double amount, StatType statType, bool isPercent)
        {
            Value.Value = amount;
            StatType = statType;
            IsPercent = isPercent;
        }

        private void handlePostConstruction()
        {
            double value = 0;

            switch (StatType)
            {
                case StatType.Health:
                    value = 25;
                    break;

                case StatType.Attack:
                    value = 10;
                    break;

                case StatType.Defense:
                    value = 12;
                    break;

                case StatType.CritRate:
                    value = 2;
                    IsPercent = false;
                    break;

                case StatType.CritDamage:
                    value = 2.5;
                    IsPercent = false;
                    break;

                case StatType.Speed:
                    value = 8;
                    IsPercent = true;
                    break;

                case StatType.AttackSpeed:
                    value = 8;
                    IsPercent = true;
                    break;
            }

            if (IsPercent) value /= 4;
            handleValue(value);
        }

        private void handleValue(double value)
        {
            Value.Value = (ParentEntity.StarRating.Value * 0.8f) * Level * value;
        }

        public void Improve()
        {
            Level++;
        }

        public static StatType GetRandomStat()
        {
            Random random = new Random();
            Array values = Enum.GetValues(typeof(StatType));
            StatType randomType = (StatType)values.GetValue(random.Next(values.Length))!;
            return randomType;
        }
    }
}
