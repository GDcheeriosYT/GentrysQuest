﻿namespace GentrysQuest.Game.Entity
{
    public class Experience(Xp xp, Level level)
    {
        public Xp Xp { get; } = xp;
        public Level Level { get; } = level;

        public Experience()
            : this(new Xp(0), new Level(0))
        {
        }
    }
}
