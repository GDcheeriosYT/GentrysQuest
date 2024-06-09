using GentrysQuest.Game.Content.Effects;
using GentrysQuest.Game.Content.Weapons;
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
            Stats.CritRate.point = 1;
            Stats.CritDamage.point = 1;

            OnSwapWeapon += checkWeapon;
            OnLevelUp += checkWeapon;
            Artifacts.OnChangeArtifact += checkWeapon;

            TextureMapping.Add("Icon", "brayden_idle.png");
            TextureMapping.Add("Idle", "brayden_idle.png");

            AudioMapping.Add("Spawn", "Brayden_Messerschmidt_Spawn.mp3");
            AudioMapping.Add("Damage", "Brayden_Messerschmidt_Damage.mp3");
        }

        private void checkWeapon()
        {
            RemoveEffect("Brayden Boost");

            if (Weapon != null && Weapon.GetType() == typeof(BraydensOsuPen))
            {
                AddEffect(new BraydenBoost
                {
                    Stack = 1 + Difficulty
                });
            }
        }
    }
}
