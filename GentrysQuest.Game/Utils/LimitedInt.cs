namespace GentrysQuest.Game.Utils
{
    public class LimitedInt
    {
        private int max;
        private int min;
        private int value;

        public LimitedInt(int max, int min)
        {
            this.max = max;
            this.min = min;
        }

        public int Value
        {
            get => value;
            set
            {
                if (value > max) this.value = max;
                else if (value < min) this.value = min;
                else this.value = value;
            }
        }
    }
}
