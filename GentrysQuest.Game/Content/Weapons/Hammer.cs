using GentrysQuest.Game.Entity.Weapon;
using osu.Framework.Graphics;
using osuTK;

namespace GentrysQuest.Game.Content.Weapons
{
    public class Hammer : Weapon
    {
        public override string Type { get; } = "Hammer";
        public override string Name { get; set; } = "Hammer";
        public override int Distance { get; set; } = 200;
        public override string Description { get; protected set; } = "Just a hammer";

        public Hammer()
        {
            Damage.SetDefaultValue(45);

            TextureMapping.Add("Icon", "weapons_hammer.png");
            TextureMapping.Add("Base", "weapons_hammer.png");

            Vector2 size = new Vector2(0.8f, 1.2f);

            AttackPattern.AddCase(1);
            AttackPattern.Add(new AttackPatternEvent { Direction = 115, Distance = 100, Size = size });
            AttackPattern.Add(new AttackPatternEvent(100) { Direction = 100, Distance = 100, Size = size, DoesDamage = false });
            AttackPattern.Add(new AttackPatternEvent(850) { Direction = -90, Distance = 100, Size = size, Transition = Easing.In, MovementSpeed = 0.2f });
            AttackPattern.Add(new AttackPatternEvent(50) { Direction = -115, Distance = 100, Size = size, Transition = Easing.Out, MovementSpeed = 0.2f });

            AttackPattern.AddCase(2);
            AttackPattern.Add(new AttackPatternEvent { Direction = -115, Distance = 100, Size = size });
            AttackPattern.Add(new AttackPatternEvent(100) { Direction = -100, Distance = 100, Size = size, DoesDamage = false });
            AttackPattern.Add(new AttackPatternEvent(850) { Direction = 90, Distance = 100, Size = size, Transition = Easing.In, MovementSpeed = 0.2f });
            AttackPattern.Add(new AttackPatternEvent(50) { Direction = 115, Distance = 100, Size = size, Transition = Easing.Out, MovementSpeed = 0.2f });
        }
    }
}
