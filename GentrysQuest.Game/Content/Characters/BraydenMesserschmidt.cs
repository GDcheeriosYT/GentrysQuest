using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Characters
{
    public class BraydenMesserschmidt : Character
    {
        public BraydenMesserschmidt()
        {
            name = "Brayden Messerschmidt";
            textureMapping.Add("Idle", "brayden_idle.png");
            starRating = new StarRating(5);
        }
    }
}
