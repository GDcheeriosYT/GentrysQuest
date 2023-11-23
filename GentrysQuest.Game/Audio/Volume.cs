using GentrysQuest.Game.Utils;

namespace GentrysQuest.Game.Audio
{
    public class Volume
    {
        private LimitedDouble amount = new LimitedDouble(1.0, 0);

        public Volume(double amount)
        {
            this.amount.Value = amount;
        }

        public double Amount
        {
            get => amount.Value;
            set => amount.Value = value;
        }
    }
}
