using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Enemies
{
    public class TestEnemy : Enemy
    {
        public TestEnemy(int starRating)
        {
            Name = "Test Enemy";
            StarRating = new StarRating(starRating);
        }
    }
}