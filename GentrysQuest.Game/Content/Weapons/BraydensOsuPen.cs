using GentrysQuest.Game.Content.Effects;
using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Weapon;
using GentrysQuest.Game.Utils;
using osu.Framework.Graphics;
using osuTK;

namespace GentrysQuest.Game.Content.Weapons
{
    public class BraydensOsuPen : Weapon
    {
        public override string Type { get; } = "Pen";
        public override int Distance { get; set; } = 200;
        public override string Name { get; protected set; } = "Brayden's Osu Pen";

        public override string Description { get; protected set; } = "The man himself, Brayden's osu pen! "
                                                                     + "When held by the true wielder, gain a 20% + 20 per difficulty increase in all your stats. "
                                                                     + "On the last attack you have a 20% chance to bleed enemies for 6 seconds. "
                                                                     + "On a critical hit you get a small boost of speed";

        public override StarRating StarRating { get; protected set; } = new(5);

        public BraydensOsuPen()
        {
            Damage.SetDefaultValue(46);

            #region Design

            Origin = Anchor.BottomCentre;

            #endregion

            #region AttackPattern

            var distance = 0.35f;
            var time = (int)MathBase.SecondToMs(0.95); // seconds
            var movementSpeed = 0.5f;
            OnHitEffect lastComboEffect = new OnHitEffect(20)
            {
                Effect = new Bleed(6)
            };

            AttackPattern.AddCase(1);
            AttackPattern.Add(new AttackPatternEvent { Direction = -90, Distance = distance, HitboxSize = new Vector2(0.1f, 1), MovementSpeed = movementSpeed });
            AttackPattern.Add(new AttackPatternEvent(time) { Direction = 90, Distance = distance, Transition = Easing.InOutQuart, HitboxSize = new Vector2(0.1f, 1), MovementSpeed = movementSpeed });

            AttackPattern.AddCase(2);
            AttackPattern.Add(new AttackPatternEvent { Direction = 90, Distance = distance, HitboxSize = new Vector2(0.1f, 1), MovementSpeed = movementSpeed });
            AttackPattern.Add(new AttackPatternEvent(time)
                { Direction = -90, Distance = distance, Transition = Easing.InOutQuart, HitboxSize = new Vector2(0.1f, 1), DamagePercent = 5, MovementSpeed = movementSpeed });

            AttackPattern.AddCase(3);
            AttackPattern.Add(new AttackPatternEvent
                { Direction = -90, Distance = distance, HitboxSize = new Vector2(0.1f, 1), MovementSpeed = movementSpeed, OnHitEffect = lastComboEffect });
            AttackPattern.Add(new AttackPatternEvent((int)(time / 1.6))
            {
                Direction = 180, Distance = distance, Transition = Easing.InSine, HitboxSize = new Vector2(0.1f, 1), DamagePercent = 15, MovementSpeed = movementSpeed, OnHitEffect = lastComboEffect
            });
            AttackPattern.Add(new AttackPatternEvent((int)(time / 1.6))
            {
                Direction = 360, Distance = distance, Transition = Easing.OutSine, HitboxSize = new Vector2(0.1f, 1), DamagePercent = 15, ResetHitBox = true, MovementSpeed = movementSpeed,
                OnHitEffect = lastComboEffect
            });

            #endregion

            #region cases

            OnHitEntity += details =>
            {
                if (details.IsCrit) Holder.AddEffect(new Swiftness(1));
            };

            #endregion

            #region TextureMapping

            TextureMapping.Add("Icon", "brayden_osu_pen_base.png");
            TextureMapping.Add("Base", "brayden_osu_pen_base.png");

            #endregion
        }
    }
}
