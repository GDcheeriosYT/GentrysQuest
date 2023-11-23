using JetBrains.Annotations;

namespace GentrysQuest.Game.Entity
{
    public class EntityBase
    {
        protected string name;

        [CanBeNull]
        protected StarRating starRating;

        protected string description;
        protected Experience experience;
    }
}
