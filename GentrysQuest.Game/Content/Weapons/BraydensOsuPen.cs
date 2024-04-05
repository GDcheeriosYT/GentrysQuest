using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Weapon;
using GentrysQuest.Game.Utils;
using osu.Framework.Graphics;
using osuTK;

namespace GentrysQuest.Game.Content.Weapons
{
    public class BraydensOsuPen : Weapon
    {
        public BraydensOsuPen()
        {
            Name = "Brayden's Osu Pen";
            StarRating = new StarRating(5);
            Description = "A osu pen";
            Damage.SetDefaultValue(50);

            #region Design

            Origin = Anchor.BottomCentre;

            #endregion

            #region AttackPattern

            var distance = 0.35f;
            var time = (int)MathBase.SecondToMs(0.95); // seconds

            AttackPattern.AddCase(1);
            AttackPattern.Add(new AttackPatternEvent { Direction = -90, Distance = distance, HitboxSize = new Vector2(0.1f, 1) });
            AttackPattern.Add(new AttackPatternEvent(time) { Direction = 90, Distance = distance, Transition = Easing.InOutQuart, HitboxSize = new Vector2(0.1f, 1) });

            AttackPattern.AddCase(2);
            AttackPattern.Add(new AttackPatternEvent { Direction = 90, Distance = distance, HitboxSize = new Vector2(0.1f, 1) });
            AttackPattern.Add(new AttackPatternEvent(time) { Direction = -90, Distance = distance, Transition = Easing.InOutQuart, HitboxSize = new Vector2(0.1f, 1), DamagePercent = 5 });

            AttackPattern.AddCase(3);
            AttackPattern.Add(new AttackPatternEvent { Direction = -90, Distance = distance, HitboxSize = new Vector2(0.1f, 1) });
            AttackPattern.Add(new AttackPatternEvent((int)(time / 1.2)) { Direction = 360, Distance = distance, Transition = Easing.InOutSine, HitboxSize = new Vector2(0.1f, 1), DamagePercent = 15 });

            #endregion

            #region TextureMapping

            TextureMapping.Add("Base", "brayden_osu_pen_base.png");

            #endregion
        }
    }
}