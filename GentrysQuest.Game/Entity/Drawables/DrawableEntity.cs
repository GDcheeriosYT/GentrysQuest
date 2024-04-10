using System.Collections.Generic;
using GentrysQuest.Game.Audio;
using GentrysQuest.Game.Graphics.TextStyles;
using GentrysQuest.Game.Utils;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Audio.Sample;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Audio;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    /// <summary>
    /// The part of the entity that we see
    /// </summary>
    public partial class DrawableEntity : CompositeDrawable
    {
        /// <summary>
        /// The entity reference
        /// </summary>
        protected readonly Entity Entity;

        /// <summary>
        /// The entity sprite
        /// </summary>
        protected readonly Sprite Sprite;

        /// <summary>
        /// The overhead of the entity
        /// </summary>
        private readonly DrawableEntityBar entityBar;

        /// <summary>
        /// The visual weapon
        /// </summary>
        protected DrawableWeapon weapon;

        /// <summary>
        /// The affiliation.
        /// Is it an opp?
        /// </summary>
        public readonly AffiliationType Affiliation;

        protected HitBox hitBox;

        /// <summary>
        /// The entity list to check when attacking
        /// </summary>
        [CanBeNull]
        protected List<DrawableEntity> EntitiesHitCheckList;

        // stat modifiers
        /// <summary>
        /// The base speed variable for all entities
        /// </summary>
        protected const double SPEED_MAIN = 0.25;

        /// <summary>
        /// A drawable entity
        /// </summary>
        /// <param name="entity">The entity reference</param>
        /// <param name="affiliationType">Entity Affiliation</param>
        /// <param name="showInfo">Will overhead info be shown on screen?</param>
        public DrawableEntity(Entity entity, AffiliationType affiliationType = AffiliationType.None, bool showInfo = true)
        {
            Entity = entity;
            Affiliation = affiliationType;
            Size = new Vector2(100);
            hitBox = new HitBox(this);
            Colour = Colour4.White;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            InternalChildren = new Drawable[]
            {
                Sprite = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                },
                entityBar = new DrawableEntityBar(Entity),
                hitBox
            };
            if (Entity.Weapon != null) weapon = new DrawableWeapon(Entity.Weapon, Affiliation);
            Entity.OnSwapWeapon += setDrawableWeapon;
            entity.OnDamage += delegate(int amount) { addIndicator(amount, DamageType.Damage); };
            entity.OnHeal += delegate(int amount) { addIndicator(amount, DamageType.Heal); };
            entity.OnCrit += delegate(int amount) { addIndicator(amount, DamageType.Crit); };
            entity.OnDeath += delegate { Sprite.FadeOut(100); };
            entity.OnSpawn += delegate { Sprite.FadeIn(100); };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures, ISampleStore samples)
        {
            // textures
            Sprite.Colour = Colour4.White;
            Sprite.Texture = textures.Get(Entity.TextureMapping.Get("Idle"));

            // sounds
            Entity.OnSpawn += delegate { AudioManager.PlaySound(new DrawableSample(samples.Get(Entity.AudioMapping.Get("Spawn")))); };
            Entity.OnDamage += delegate { AudioManager.PlaySound(new DrawableSample(samples.Get(Entity.AudioMapping.Get("Damage")))); };
            Entity.OnLevelUp += delegate { AudioManager.PlaySound(new DrawableSample(samples.Get(Entity.AudioMapping.Get("Levelup")))); };
            Entity.OnDeath += delegate { AudioManager.PlaySound(new DrawableSample(samples.Get(Entity.AudioMapping.Get("Death")))); };
        }

        /// <summary>
        /// Attacks towards a direction
        /// </summary>
        /// <param name="position">Location of the attack</param>
        public void Attack(Vector2 position)
        {
            Vector2 center = new Vector2(50);
            double angle = MathBase.GetAngle(center, position);
            if (weapon.GetWeaponObject().CanAttack) weapon.Attack((float)angle + 90);
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

        private void setDrawableWeapon()
        {
            if (weapon != null) RemoveInternal(weapon, false);

            weapon = new DrawableWeapon(Entity.Weapon, Affiliation);
            weapon.Affiliation = Affiliation;
            AddInternal(weapon);
        }

        /// <summary>
        /// Set the EntityHitList
        /// should be used by some test class or by Gameplay class
        /// </summary>
        /// <param name="entities">The list of entities</param>
        public void SetEntities(List<DrawableEntity> entities)
        {
            EntitiesHitCheckList = entities;
        }

        /// <summary>
        /// In some cases you'll want to get the entity reference for this drawable class
        /// </summary>
        /// <returns>The entity reference for this drawable</returns>
        public Entity GetEntityObject() => Entity;

        /// <summary>
        /// Manages the speed of the entity
        /// </summary>
        /// <returns></returns>
        public double GetSpeed()
        {
            return SPEED_MAIN * Entity.Stats.Speed.CurrentValue;
        }
    }
}
