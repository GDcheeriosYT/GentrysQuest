using System;

namespace GentrysQuest.Game.Utils
{
    public class TimeEvent(int timeMS, Action theEvent)
    {
        private readonly int timeMS = timeMS;
        private readonly Action theEvent = theEvent;

        public void Activate()
        {
            theEvent();
        }
    }
}
