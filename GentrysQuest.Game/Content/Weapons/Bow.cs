using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Weapon;
using GentrysQuest.Game.Utils;
using osuTK;

namespace GentrysQuest.Game.Content.Weapons
{
    public class Bow : Weapon
    {
        public override string Type { get; } = "Bow";
        public override int Distance { get; set; } = 1000;
        public override string Name { get; set; } = "Bow";
        public override string Description { get; protected set; } = "Just a bow.";
        public override bool IsGeneralDamageMode { get; protected set; } = false;

        public Bow()
        {
            Damage.SetDefaultValue(20);
            ChargeTime = new Second(1.5);

            AttackPattern.AddCase(1);
            AttackPattern.Add(new AttackPatternEvent(100));
            AttackPattern.Add(new AttackPatternEvent(500)
            {
                Size = new Vector2(0.5f),
                Distance = 50,
                HitboxSize = new Vector2(0),
                Projectiles =
                [
                    new ProjectileParameters
                    {
                        Speed = 15,
                        Lifetime = 500,
                        PassthroughAmount = 1,
                        Damage = (int)Damage.Current.Value / 10,
                        TakesHolderDamage = true,
                        TakesDefense = true
                    }
                ]
            });

            ChargeAttackPattern.Add(new AttackPatternEvent(20)
            {
                Distance = 50
            });
        }
    }
}
