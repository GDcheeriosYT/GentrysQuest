using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Weapon;
using GentrysQuest.Game.Utils;
using osu.Framework.Graphics;
using osuTK;

namespace GentrysQuest.Game.Content.Weapons
{
    public class Knife : Weapon
    {
        public Knife()
        {
            Name = "Knife";
            StarRating = new StarRating(1);
            Description = "Just a knife...";
            Damage.SetDefaultValue(16);

            #region Design

            Origin = Anchor.BottomCentre;

            #endregion

            #region AttackPattern

            var time = (int)MathBase.SecondToMs(0.4); // seconds

            AttackPattern.AddCase(1);
            AttackPattern.Add(new AttackPatternEvent { Distance = 0, HitboxSize = new Vector2(0.1f, 1), Size = new Vector2(0.6f) });
            AttackPattern.Add(new AttackPatternEvent(time) { Distance = 0.4f, HitboxSize = new Vector2(0.2f, 1), Size = new Vector2(0.6f) });
            AttackPattern.Add(new AttackPatternEvent(time) { Distance = 0, HitboxSize = new Vector2(0.1f, 1), Size = new Vector2(0.6f) });

            #endregion

            #region TextureMapping

            TextureMapping.Add("Base", "knife.png");

            #endregion
        }
    }
}
