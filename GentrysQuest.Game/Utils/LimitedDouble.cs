namespace GentrysQuest.Game.Utils
{
    public class LimitedDouble
    {
        private double max;
        private double min;
        private double value;

        public LimitedDouble(double max, double min)
        {
            this.max = max;
            this.min = min;
        }

        public double Value
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
