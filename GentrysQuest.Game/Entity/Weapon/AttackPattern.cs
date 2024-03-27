using System.Collections.Generic;
using GentrysQuest.Game.Entity.Drawables;
using GentrysQuest.Game.Utils;

namespace GentrysQuest.Game.Entity.Weapon
{
    public class AttackPattern
    {
        private DrawableWeapon weaponInstance;
        private List<AttackPatternCaseHolder> caseEventList = new List<AttackPatternCaseHolder>();
        private AttackPatternCaseHolder selectedCaseHolder;

        public void AddCase(int caseNumber)
        {
            AttackPatternCaseHolder thePattern = new AttackPatternCaseHolder(caseNumber);
            selectedCaseHolder = thePattern;
            caseEventList.Add(thePattern);
        }

        public void SetCaseHolder(int caseNumber)
        {
            selectedCaseHolder = GetCase(caseNumber);
        }

        public void Add(int timeMS, AttackPatternEvent attackPatternEvent)
        {
            selectedCaseHolder.AddEvent(new TimeEvent(timeMS, delegate { attackPatternEvent.Activate(timeMS); }));
        }

        public AttackPatternCaseHolder GetCase(int caseNumber)
        {
            foreach (AttackPatternCaseHolder caseHolder in caseEventList)
            {
                if (caseHolder.attackNumberCase == caseNumber)
                {
                    return caseHolder;
                }
            }

            return null;
        }

        private void setWeaponInstance(DrawableWeapon theWeapon)
        {
            weaponInstance = theWeapon;

            foreach (AttackPatternCaseHolder caseHolder in caseEventList)
            {
                caseHolder.
            }
        }
    }
}
