using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Characters
{
    public class TestCharacter : Character
    {
        public TestCharacter(int starRating)
        {
            Name = "Test Character";
            Description = "Just the character used for testing and stuff...";
            StarRating = new StarRating(starRating);
        }
    }
}
