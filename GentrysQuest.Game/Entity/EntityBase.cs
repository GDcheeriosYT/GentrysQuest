using GentrysQuest.Game.Graphics;

namespace GentrysQuest.Game.Entity
{
    public class EntityBase
    {
        public string Name { get; protected set; } = "Entity";
        public StarRating StarRating { get; protected set; } = new StarRating(1);
        public string Description { get; protected set; } = "This is a description";
        public Experience Experience { get; protected set; }
        public TextureMapping TextureMapping { get; protected set; } = new();
        public AudioMapping AudioMapping { get; protected set; } = new();
    }
}
