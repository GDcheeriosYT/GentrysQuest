using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Characters
{
    public class TestCharacter : Character
    {
        public TestCharacter(int starRating)
        {
            name = "Test Character";
            description = "Just the character used for testing and stuff...";
            this.starRating = new StarRating(starRating);
        }
    }
}
