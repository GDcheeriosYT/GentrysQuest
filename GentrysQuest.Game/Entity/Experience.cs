namespace GentrysQuest.Game.Entity
{
    public class Experience
    {
        public Xp xp { get; }
        public Level level { get; }

        public Experience(Xp xp, Level level)
        {
            this.xp = xp;
            this.level = level;
        }
    }
}

