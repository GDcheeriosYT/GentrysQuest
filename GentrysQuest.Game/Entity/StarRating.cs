using GentrysQuest.Game.Utils;

namespace GentrysQuest.Game.Entity
{
    public class StarRating
    {
        private LimitedInt value = new LimitedInt(5, 1);

        public StarRating(int value)
        {
            this.value.Value = value;
        }

        public int Value
        {
            get => value.Value;
            set => this.value.Value = value;
        }
    }
}
