using GentrysQuest.Game.Graphics;

namespace GentrysQuest.Game.Entity
{
    public class EntityBase
    {
        public string Name { get; protected set; } = "Entity";

        public StarRating StarRating { get; protected set; } = new StarRating(1);

        public string Description { get; protected set; } = "This is a description";
        public Experience experience { get; protected set; }

        // textures
        public TextureMapping TextureMapping { get; protected set; } = new();
    }
}
