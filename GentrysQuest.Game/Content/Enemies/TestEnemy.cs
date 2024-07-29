using GentrysQuest.Game.Content.Weapons;
using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Enemies
{
    public class TestEnemy : Enemy
    {
        public TestEnemy(int starRating)
        {
            Name = "Test Enemy";
            StarRating = new StarRating(starRating);

            WeaponChoices.AddChoice(new Bow(), 33);
            WeaponChoices.AddChoice(new Knife(), 33);
            WeaponChoices.AddChoice(new Sword(), 33);
        }
    }
}
