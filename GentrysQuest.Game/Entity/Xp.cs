namespace GentrysQuest.Game.Entity
{
    public class Xp
    {
        public int current { get; private set; }
        public int requirement { get; private set; }
        public double progress { get; private set; }

        public Xp(int current)
        {
            this.current = current;
        }

        public bool add_xp(int amount)
        {
            current += amount;

            if (current >= requirement)
            {
                current -= requirement;
                progress = current / requirement;
                return true;
            }

            progress = current / requirement;
            return false;
        }
    }
}
