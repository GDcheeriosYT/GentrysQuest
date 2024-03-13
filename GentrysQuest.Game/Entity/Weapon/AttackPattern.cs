using System.Collections.Generic;
using GentrysQuest.Game.Entity.Drawables;
using GentrysQuest.Game.Utils;
using JetBrains.Annotations;

namespace GentrysQuest.Game.Entity.Weapon
{
    public class AttackPattern
    {
        [CanBeNull]
        private DrawableWeapon instanceWeapon;

        private readonly List<TimeEvent> timingEvents;

        public AttackPattern()
        {
            // instanceWeapon = instance;
        }

        public void SetDrawableInstance(DrawableWeapon drawableWeapon)
        {
            instanceWeapon = drawableWeapon;
        }

        public void InitiatePattern(int amount)
        {
        }
    }
}
