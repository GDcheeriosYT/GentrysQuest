using osu.Framework.Logging;

namespace GentrysQuest.Game.Entity
{
    /// <summary>
    /// Xp management class
    /// </summary>
    public class Xp
    {
        public int Current { get; private set; }
        public int Requirement;
        public double Progress { get; private set; }

        public Xp(int current)
        {
            this.Current = current;
        }

        public bool add_xp(int amount)
        {
            Current += amount;
            Logger.Log($"{Current} {Requirement}");

            if (Current >= Requirement)
            {
                Current -= Requirement;
                Progress = Current / Requirement;
                return true;
            }

            Progress = Current / Requirement;
            return false;
        }

        public void CalculateRequirment(int level, int starRating)
        {
            int difficulty = 1 + (int)(level / 20);
            int starRatingExperience = starRating * 25;
            int levelExperience = level * 10;

            Requirement = difficulty * (levelExperience * starRatingExperience);
        }
    }
}
