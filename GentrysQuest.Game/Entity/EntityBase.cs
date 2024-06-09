using GentrysQuest.Game.Graphics;

namespace GentrysQuest.Game.Entity
{
    public class EntityBase
    {
        public virtual string Name { get; protected set; } = "Entity";
        public virtual StarRating StarRating { get; protected set; } = new StarRating(1);
        public virtual string Description { get; protected set; } = "This is a description";
        public Experience Experience { get; protected set; } = new();
        public TextureMapping TextureMapping { get; protected set; } = new();
        public AudioMapping AudioMapping { get; protected set; } = new();
        public byte Difficulty { get; protected set; } = 0;

        public delegate void EntityEvent();

        // Experience events
        public event EntityEvent OnGainXp;
        public event EntityEvent OnLevelUp;

        public void AddXp(int amount)
        {
            while (Experience.Xp.add_xp(ref amount)) LevelUp();
            OnGainXp?.Invoke();
        }

        public void LevelUp()
        {
            Experience.Level.AddLevel();
            CalculateXpRequirement();
            Difficulty = (byte)(Experience.Level.Current.Value / 20);

            OnLevelUp?.Invoke();
        }

        public void CalculateXpRequirement() => Experience.Xp.CalculateRequirement(Experience.Level.Current.Value, StarRating.Value);
    }
}
