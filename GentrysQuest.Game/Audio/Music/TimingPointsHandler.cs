using System.Collections.Generic;

namespace GentrysQuest.Game.Audio.Music
{
    public class TimingPointsHandler
    {
        private readonly List<TimingPoint> timingPoints = [];

        public void AddPoint(TimingPoint newPoint) => timingPoints.Add(newPoint);
    }
}
