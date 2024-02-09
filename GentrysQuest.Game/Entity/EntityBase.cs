using GentrysQuest.Game.Graphics;
using JetBrains.Annotations;

namespace GentrysQuest.Game.Entity
{
    public class EntityBase
    {
        public string name { get; protected set; }

        [CanBeNull]
        public StarRating starRating { get; protected set; }

        public string description { get; protected set; }
        public Experience experience { get; protected set; }

        // textures
        public TextureMapping textureMapping { get; protected set; } = new();
    }
}
