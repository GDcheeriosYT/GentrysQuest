using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Weapon;

namespace GentrysQuest.Game.Content.Weapons
{
    public class BraydensOsuPen : Weapon
    {
        public BraydensOsuPen()
        {
            Name = "Brayden's Osu Pen";
            StarRating = new StarRating(5);
            Description = "A osu pen";

            #region AttackPattern

            AttackPattern.AddCase(1);
            AttackPattern.Add(0, new AttackPatternEvent { Direction = -90, Distance = 0.5f });
            AttackPattern.Add(1000, new AttackPatternEvent { Direction = 90, Distance = 0.5f });

            AttackPattern.AddCase(2);
            AttackPattern.Add(1000, new AttackPatternEvent { Direction = -90, Distance = 0.5f });

            #endregion

            #region TextureMapping

            TextureMapping.Add("Base", "brayden_osu_pen_base.png");

            #endregion
        }
    }
}
