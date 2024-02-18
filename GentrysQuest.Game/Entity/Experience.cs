namespace GentrysQuest.Game.Entity
{
    public class Experience
    {
        public Xp Xp { get; }
        public Level Level { get; }

        public Experience(Xp xp, Level level)
        {
            this.Xp = xp;
            this.Level = level;
        }
    }
}
