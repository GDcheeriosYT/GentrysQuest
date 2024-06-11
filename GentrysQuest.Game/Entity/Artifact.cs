using System;
using System.Collections.Generic;
using GentrysQuest.Game.Utils;

namespace GentrysQuest.Game.Entity
{
    public class Artifact : EntityBase
    {
        public Family Family { get; private set; } = null;
        public Buff MainAttribute { get; set; }
        public List<Buff> Attributes { get; set; }
        public virtual List<StatType> ValidMainAttributes { get; set; } = new();
        public virtual List<int> ValidStarRatings { get; set; } = new() { 1, 2, 3, 4, 5 };
        public virtual AllowedPercentMethod AllowedPercentMethod { get; set; } = AllowedPercentMethod.Allowed;
        public Character Holder;

        public void SetFamily(Family family)
        {
            if (Family != null) Family = family;
        }

        public Artifact()
        {
            Initialize(ValidStarRatings[Random.Shared.Next(ValidStarRatings.Count)]);
            OnLevelUp += delegate
            {
                if (Experience.Level.Current.Value % 4 == 0) addBuff();
                MainAttribute.Improve();
                Holder?.UpdateStats();
            };
        }

        public void Initialize(int starRating)
        {
            StarRating.Value = starRating;
            StatType stat = Buff.GetRandomStat();
            bool isPercent = false;

            if (ValidMainAttributes.Count > 0) stat = ValidMainAttributes[Random.Shared.Next(ValidMainAttributes.Count)];

            switch (AllowedPercentMethod)
            {
                case AllowedPercentMethod.Allowed:
                    isPercent = MathBase.RandomBool();
                    break;

                case AllowedPercentMethod.NotAllowed:
                    break;

                case AllowedPercentMethod.OnlyPercent:
                    isPercent = true;
                    break;
            }

            MainAttribute = new Buff(this, stat, isPercent);

            initializeAttributes();
        }

        private void initializeAttributes()
        {
            Attributes = new();
            Experience = new Experience(new Xp(), new Level(1, StarRating.Value * 4));
            int counter = StarRating.Value;

            while (counter > 2)
            {
                addBuff();
                counter--;
            }
        }

        private void addBuff(Buff buff) => handleBuff(buff);

        private void addBuff() => handleBuff(new Buff(this));

        private void handleBuff(Buff newBuff)
        {
            bool duplicate = false;

            foreach (Buff buff in Attributes)
            {
                if (newBuff.StatType == buff.StatType && newBuff.IsPercent == buff.IsPercent)
                {
                    buff.Improve();
                    duplicate = true;
                }
            }

            if (!duplicate)
            {
                newBuff.ParentEntity = this;
                Attributes.Add(newBuff);
            }
        }
    }
}
