using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Characters
{
    public class BraydenMesserschmidt : Entity.Entity
    {
        public BraydenMesserschmidt()
        {
            name = "Brayden Messerschmidt";
            textureMapping.Add("Idle", "brayden_idle.png");
            starRating = new StarRating(5);
        }
    }
}
