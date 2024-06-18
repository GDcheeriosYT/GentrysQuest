using System.Collections.Generic;
using GentrysQuest.Game.Utils;
using osu.Framework.Logging;

namespace GentrysQuest.Game.Entity
{
    public class WeaponChoices
    {
        private List<Weapon.Weapon> weapons = new();
        private List<int> chanceOfPicking = new();

        public void AddChoice(Weapon.Weapon weapon, int chanceOfPicking)
        {
            weapons.Add(weapon);
            this.chanceOfPicking.Add(chanceOfPicking);
        }

        public Weapon.Weapon GetChoice()
        {
            Logger.Log("zebra");
            while (true)
            {
                for (int i = 0; i < weapons.Count; i++)
                {
                    Logger.Log("Dog");

                    if (MathBase.IsChanceSuccessful(chanceOfPicking[i], 100))
                    {
                        Logger.Log("Pig");
                        return weapons[i];
                    }
                }
            }
        }
    }
}
