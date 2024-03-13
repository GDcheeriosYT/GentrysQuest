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

            // AttackPattern = new AttackPattern()

            #endregion

            #region TextureMapping

            TextureMapping.Add("Base", "brayden_osu_pen_base.png");

            #endregion
        }
    }
}
