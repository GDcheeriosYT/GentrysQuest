using osu.Framework.Bindables;

namespace GentrysQuest.Game.Entity
{
    public class Level
    {
        public Bindable<int> Current { get; private set; } = new Bindable<int>(0);
        public Bindable<int> Limit { get; } = new Bindable<int>(0);

        public Level(int current = 1)
        {
            Current.Value = current;
            Limit.Value = 0;
        }

        private Level(int current, int limit)
        {
            Current.Value = current;
            Limit.Value = limit;
        }

        public static Level CreateInstance(int current, int limit)
        {
            return new Level(current, limit);
        }

        public void AddLevel()
        {
            if (Current != Limit) Current.Value++;
        }
    }
}
