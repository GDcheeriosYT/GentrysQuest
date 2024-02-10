namespace GentrysQuest.Game.Entity
{
    public class Level
    {
        public int current { get; private set; }
        public int limit { get; }

        public Level(int current)
        {
            this.current = current;
            limit = 0;
        }

        public Level(int current, int limit)
        {
            this.current = current;
            this.limit = limit;
        }

        public void AddLevel()
        {
            if (current != limit) current++;
        }
    }
}
