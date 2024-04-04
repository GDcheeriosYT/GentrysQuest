using System.Collections.Generic;

namespace GentrysQuest.Game.Entity.Weapon
{
    public class AttackPattern
    {
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

        public void Add(AttackPatternEvent attackPatternEvent)
        {
            selectedCaseHolder.AddEvent(attackPatternEvent);
        }

        public AttackPatternCaseHolder GetCase(int caseNumber)
        {
            foreach (AttackPatternCaseHolder caseHolder in caseEventList)
            {
                if (caseHolder.AttackNumberCase == caseNumber)
                {
                    return caseHolder;
                }
            }

            return null;
        }
    }
}
