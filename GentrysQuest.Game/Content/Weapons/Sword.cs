using GentrysQuest.Game.Entity.Weapon;
using osu.Framework.Graphics;
using osuTK;

namespace GentrysQuest.Game.Content.Weapons
{
    public class Sword : Weapon
    {
        public override string Type { get; } = "Sword";
        public override int Distance { get; set; } = 65;
        public override string Description { get; protected set; } = "Just a sword";

        public Sword()
        {
            Damage.SetDefaultValue(22);

            Vector2 hitboxSize = new Vector2(0.25f, 1);

            AttackPattern.AddCase(1);
            AttackPattern.Add(new AttackPatternEvent { Direction = 90, Distance = 100, HitboxSize = hitboxSize });
            AttackPattern.Add(new AttackPatternEvent(650) { Direction = -90, Distance = 100, HitboxSize = hitboxSize, Transition = Easing.Out });

            AttackPattern.AddCase(2);
            AttackPattern.Add(new AttackPatternEvent { Direction = -90, Distance = 100, HitboxSize = hitboxSize });
            AttackPattern.Add(new AttackPatternEvent(650) { Direction = 90, Distance = 100, HitboxSize = hitboxSize, Transition = Easing.Out });
        }
    }
}
