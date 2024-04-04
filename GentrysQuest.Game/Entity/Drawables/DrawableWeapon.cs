using System.Collections.Generic;
using GentrysQuest.Game.Entity.Weapon;
using GentrysQuest.Game.Utils;
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
        protected float distance;

        public DrawableWeapon(Weapon.Weapon weapon)
        {
            Weapon = weapon;
            WeaponHitBox = new HitBox(AffiliationType.None);
            Size = new Vector2(1f);
            RelativeSizeAxes = Axes.Both;
            Colour = Colour4.White;
            Anchor = Anchor.Centre;
            Origin = weapon.origin;
            InternalChildren = new Drawable[]
            {
                Sprite = new Sprite
                {
                    RelativeSizeAxes = Axes.Both
                },
                WeaponHitBox
            };
            Weapon.CanAttack = true;
            disable();
        }

        private void disable()
        {
            WeaponHitBox.Disable();
            Hide();
        }

        private void enable()
        {
            WeaponHitBox.Enable();
            Show();
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            Sprite.Colour = Colour4.White;
            Sprite.Texture = textures.Get(Weapon.TextureMapping.Get("Base"));
        }

        public Weapon.Weapon GetWeaponObject() { return Weapon; }

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
            Weapon.CanAttack = false;
            enable();
            Weapon.AttackAmount += 1;
            AttackPatternCaseHolder caseHolder = Weapon.AttackPattern.GetCase(Weapon.AttackAmount);
            List<AttackPatternEvent> patterns;

            if (caseHolder == null)
            {
                Weapon.AttackAmount = 1;
                caseHolder = Weapon.AttackPattern.GetCase(Weapon.AttackAmount);
            }

            patterns = caseHolder.GetEvents();
            double delay = 0;

            foreach (AttackPatternEvent pattern in patterns)
            {
                double speed = pattern.TimeMs / Weapon.Holder.Stats.AttackSpeed.CurrentValue;
                Scheduler.AddDelayed(() =>
                {
                    if (pattern.Direction != null) this.RotateTo((float)pattern.Direction + direction, duration: speed, pattern.Transition);
                    if (pattern.Position != null) this.MoveTo((Vector2)pattern.Position, duration: speed, pattern.Transition);
                    if (pattern.Size != null) this.ResizeTo((Vector2)pattern.Size, duration: speed, pattern.Transition);
                    if (pattern.HitboxSize != null) this.MoveTo((Vector2)pattern.HitboxSize, duration: speed, pattern.Transition);
                    if (pattern.Distance != null) distance = (float)pattern.Distance;
                }, delay);
                delay += speed;
            }

            Scheduler.AddDelayed(() =>
            {
                Weapon.CanAttack = true;
                disable();
            }, delay);
        }

        protected override void Update()
        {
            base.Update();

            if (!Weapon.CanAttack)
            {
                Position = MathBase.GetAngleToVector(Rotation - 90) * distance;
                if (WeaponHitBox.CheckCollision())
            }
        }
    }
}
