using System.Collections.Generic;
using GentrysQuest.Game.Utils;

namespace GentrysQuest.Game.Entity.Weapon
{
    public class AttackPatternCaseHolder
    {
        public int attackNumberCase { get; private set; }
        private List<TimeEvent> caseEvents;

        public AttackPatternCaseHolder(int attackNumberCase)
        {
            this.attackNumberCase = attackNumberCase;
        }

        public void AddEvent(TimeEvent timedEvent)
        {
            caseEvents.Add(timedEvent);
        }

        public void ActivateCase(float direction)
        {
            foreach (TimeEvent timeEvent in caseEvents)
            {
                timeEvent.Activate();
            }
        }

        public void 
    }
}
