using GentrysQuest.Game.Entity.Weapon;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class DrawableWeapon : CompositeDrawable
    {
        protected readonly Weapon.Weapon Weapon;
        protected readonly Sprite Sprite;
        protected readonly HitBox WeaponHitBox;

        public DrawableWeapon(Weapon.Weapon weapon)
        {
            Weapon = weapon;
            Size = new Vector2(100);
            Colour = Colour4.White;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            InternalChildren = new Drawable[]
            {
                Sprite = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            Sprite.Colour = Colour4.White;
            Sprite.Texture = textures.Get(Weapon.TextureMapping.Get("Idle"));
        }

        /// <summary>
        /// Change the size of the weapon's hitbox
        /// The size is relative to the weapon so 1 is 1:1 size
        /// </summary>
        /// <param name="size">The size vector</param>
        public void ChangeHitBoxSize(Vector2 size)
        {
            WeaponHitBox.Size = size;
        }

        public void Attack(float direction)
        {
            handleAttackPattern(direction);
        }

        private void handleAttackPattern(float direction)
        {
            Weapon.AttackPattern.GetCase(Weapon.AttackAmount).ActivateCase();
        }
    }
}
