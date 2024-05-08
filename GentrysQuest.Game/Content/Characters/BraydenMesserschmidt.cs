using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Content.Characters
{
    public class BraydenMesserschmidt : Character
    {
        public BraydenMesserschmidt()
        {
            Name = "Brayden Messerschmidt";
            StarRating = new StarRating(5);
            Description = "An osu player who formed a contract with ppy(Dean Herbert) to not talk to females.";

            Stats.Speed.point = 1;
            Stats.AttackSpeed.point = 1;
            Stats.CritRate.point = 2;

            TextureMapping.Add("Icon", "brayden_idle.png");
            TextureMapping.Add("Idle", "brayden_idle.png");

            AudioMapping.Add("Spawn", "Brayden_Messerschmidt_Spawn.mp3");
            AudioMapping.Add("Damage", "Brayden_Messerschmidt_Damage.mp3");
        }
    }
}
