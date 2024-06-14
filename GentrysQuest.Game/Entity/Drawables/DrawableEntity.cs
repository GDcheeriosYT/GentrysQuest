using System.Collections.Generic;
using System.Linq;
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
    public partial class DrawableEntity : CompositeDrawable, IDrawableEntity
    {
        /// <summary>
        /// The entity reference
        /// </summary>
        protected readonly Entity Entity;

        /// <summary>
        /// The entity sprite
        /// </summary>
        public Sprite Sprite { get; set; }

        /// <summary>
        /// The overhead of the entity
        /// </summary>
        private readonly DrawableEntityBar entityBar;

        /// <summary>
        /// The visual weapon
        /// </summary>
        protected DrawableWeapon weapon;

        public AffiliationType Affiliation { get; set; }
        public List<Projectile> QueuedProjectiles { get; set; } = new();

        public HitBox HitBox { get; set; }
        protected CollisonHitBox colliderBox;

        protected bool Moving;
        protected Vector2 lastPosition;

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
        /// When doing some math you might need this
        /// </summary>
        public const float SLOWING_FACTOR = 0.01f;

        private double lastRegenTime;

        // Movement events
        public delegate void Movement(float direction, double speed);

        public event Movement OnMove;

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
            HitBox = new HitBox(this);
            colliderBox = new CollisonHitBox(this);
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
                HitBox,
                colliderBox
            };
            if (Entity.Weapon != null) weapon = new DrawableWeapon(this, Affiliation);
            Entity.OnSwapWeapon += setDrawableWeapon;
            entity.OnDamage += delegate(int amount) { addIndicator(amount, DamageType.Damage); };
            entity.OnHeal += delegate(int amount) { addIndicator(amount, DamageType.Heal); };
            entity.OnCrit += delegate(int amount) { addIndicator(amount, DamageType.Crit); };
            entity.OnDeath += delegate { Sprite.FadeOut(100); };
            entity.OnSpawn += delegate { Sprite.FadeIn(100); };
            entity.OnSpawn += delegate { lastRegenTime = Clock.CurrentTime; };
            entity.OnEffect += delegate
            {
                foreach (var effect in Entity.Effects.Where(effect => !effect.IsInfinite))
                {
                    effect.StartTime ??= Clock.CurrentTime;
                }
            };
            entity.UpdateStats();
            entity.Stats.Restore();
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

        private void regen()
        {
            lastRegenTime = Clock.CurrentTime;
            Entity.Heal((int)Entity.Stats.RegenStrength.Current.Value);
        }

        protected virtual void Move(float direction, double speed)
        {
            if (!Entity.CanMove) return;

            float value = (float)(Clock.ElapsedFrameTime * speed);
            colliderBox.Position += (MathBase.GetAngleToVector(direction) * 0.0005f) * value;

            if (!HitBoxScene.Collides(colliderBox)) OnMove?.Invoke(direction, speed);
        }

        /// <summary>
        /// Attacks towards a direction
        /// </summary>
        /// <param name="position">Location of the attack</param>
        public void Attack(Vector2 position)
        {
            if (!Entity.CanAttack) return;
            Vector2 center = new Vector2(50);
            double angle = MathBase.GetAngle(Position + center, position);
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
            byte size = 50;

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
                    size = 52;
                    break;
            }

            Indicator indicatorReference;
            AddInternal(indicatorReference = new Indicator(amount)
            {
                Colour = colour,
                Font = FontUsage.Default.With(size: size),
                Shadow = true
            });
            Scheduler.AddDelayed(() =>
            {
                RemoveInternal(indicatorReference, false);
            }, indicatorReference.FadeOut());
        }

        protected void Dodge()
        {
            if (Entity.CanDodge)
            {
                Entity.CanDodge = false;
                Entity.IsDodging = true;
                Entity.SpeedModifier = 3;
                Scheduler.AddDelayed(() => { Entity.SpeedModifier = 1; }, 100);
                Scheduler.AddDelayed(() => { Entity.IsDodging = false; }, 100);
                Scheduler.AddDelayed(() => { Entity.CanDodge = true; }, 1000);
            }
        }

        private void setDrawableWeapon()
        {
            if (weapon != null) RemoveInternal(weapon, true);

            if (Entity.Weapon != null)
            {
                weapon = new DrawableWeapon(this, Affiliation);
                weapon.Affiliation = Affiliation;
                AddInternal(weapon);
            }
        }

        /// <summary>
        /// Set the EntityHitList
        /// should be used by some test class or by Gameplay class
        /// </summary>
        /// <param name="entities">The list of entities</param>
        public void SetEntities(List<DrawableEntity> entities) => EntitiesHitCheckList = entities;

        /// <summary>
        /// In some cases you'll want to get the entity reference for this drawable class
        /// </summary>
        /// <returns>The entity reference for this drawable</returns>
        public Entity GetEntityObject() => Entity;

        /// <summary>
        /// Manages the speed of the entity
        /// </summary>
        /// <returns></returns>
        public double GetSpeed() => SPEED_MAIN * Entity.Stats.Speed.Current.Value * Entity.SpeedModifier;

        protected override void Update()
        {
            // Main update
            base.Update();

            // Reset collider box
            colliderBox.Position = new Vector2(0);

            // Effects logic
            Entity.Affect(Clock.CurrentTime);

            // Skills logic
            Entity.Secondary?.SetPercent(Clock.CurrentTime);
            Entity.Utility?.SetPercent(Clock.CurrentTime);
            Entity.Ultimate?.SetPercent(Clock.CurrentTime);

            // Regen should always be at the bottom
            if (Entity.IsDead || Entity.IsFullHealth) return;

            double elapsedRegenTime = Clock.CurrentTime - lastRegenTime;
            if (Entity.Stats.RegenSpeed.Current.Value == 0) return;

            if (elapsedRegenTime * Entity.Stats.RegenSpeed.Current.Value >= 1000) regen();
        }
    }
}
