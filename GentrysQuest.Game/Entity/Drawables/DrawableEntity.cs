using System;
using System.Collections.Generic;
using GentrysQuest.Game.Graphics.TextStyles;
using GentrysQuest.Game.Utils;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class DrawableEntity : CompositeDrawable
    {
        protected readonly Entity Entity;

        protected readonly Sprite Sprite;

        private readonly DrawableEntityBar entityBar;

        protected DrawableWeapon weapon;

        public readonly AffiliationType Affiliation;

        [CanBeNull]
        protected List<DrawableEntity> EntitiesHitCheckList;

        // stat modifiers
        protected const double SPEED_MAIN = 0.25;

        public Quad HitBox
        {
            get
            {
                RectangleF rect = ScreenSpaceDrawQuad.AABBFloat;
                return Quad.FromRectangle(rect);
            }
        }

        public DrawableEntity(Entity entity, AffiliationType affiliationType = AffiliationType.None, bool showInfo = true)
        {
            Entity = entity;
            Affiliation = affiliationType;
            Size = new Vector2(100);
            Colour = Colour4.White;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            InternalChildren = new Drawable[]
            {
                Sprite = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                },
                entityBar = new DrawableEntityBar(Entity)
            };
            entity.OnDamage += delegate(int amount) { addIndicator(amount, DamageType.Damage); };
            entity.OnHeal += delegate(int amount) { addIndicator(amount, DamageType.Heal); };
            entity.OnCrit += delegate(int amount) { addIndicator(amount, DamageType.Crit); };
            entity.Stats.Restore();
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            Sprite.Colour = Colour4.White;
            Sprite.Texture = textures.Get(Entity.TextureMapping.Get("Idle"));
        }

        public void Attack(Vector2 position)
        {
            Vector2 center = new Vector2(50);
            double angle = MathBase.GetAngle(center, position);
        }

        /// <summary>
        /// Adds a indicator text for when this entity heals/takes damage
        /// </summary>
        /// <param name="amount">The amount of health change</param>
        /// <param name="type">The type of damage</param>
        private void addIndicator(int amount, DamageType type)
        {
            Colour4 colour = default;

            switch (type)
            {
                case DamageType.Heal:
                    colour = Colour4.LimeGreen;
                    break;

                case DamageType.Damage:
                    colour = Colour4.White;
                    break;

                case DamageType.Crit:
                    colour = Colour4.Red;
                    break;
            }

            Indicator indicatorReference;
            AddInternal(indicatorReference = new Indicator(amount)
            {
                Colour = colour
            });
            Scheduler.AddDelayed(() =>
            {
                RemoveInternal(indicatorReference, false);
            }, indicatorReference.FadeOut());
        }

        public void SetEntities(List<DrawableEntity> entities)
        {
            EntitiesHitCheckList = entities;
        }

        public void SetWeapon(Weapon.Weapon weapon)
        {
            this.weapon = new DrawableWeapon(weapon);
            Entity.Weapon = weapon;
        }

        public Entity GetEntityObject() { return Entity; }

        public double GetSpeed()
        {
            return SPEED_MAIN * Entity.Stats.Speed.CurrentValue;
        }
    }
}
