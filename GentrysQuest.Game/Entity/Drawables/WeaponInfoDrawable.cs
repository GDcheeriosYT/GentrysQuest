using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class WeaponInfoDrawable : EntityInfoDrawable
    {
        private SpriteText damage;
        private DrawableBuffIcon mainAttributeBuffIcon;

        public WeaponInfoDrawable(Weapon.Weapon entity)
            : base(entity)
        {
            BuffContainer.Add(mainAttributeBuffIcon = new DrawableBuffIcon(entity.Buff));
        }
    }
}
