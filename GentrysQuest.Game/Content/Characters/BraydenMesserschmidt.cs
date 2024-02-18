using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Characters
{
    public class BraydenMesserschmidt : Character
    {
        public BraydenMesserschmidt()
        {
            Name = "Brayden Messerschmidt";
            TextureMapping.Add("Idle", "brayden_idle.png");
            StarRating = new StarRating(5);
        }
    }
}
