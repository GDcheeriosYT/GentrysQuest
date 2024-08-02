using System;
using System.Collections.Generic;
using System.Linq;
using GentrysQuest.Game.Audio;
using GentrysQuest.Game.Content.Effects;
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
        public DrawableWeapon Weapon;

        public AffiliationType Affiliation { get; set; }
        public List<Projectile> QueuedProjectiles { get; set; } = new();

        public HitBox HitBox { get; set; }
        public CollisonHitBox ColliderBox;

        public int DirectionLooking;
        public Vector2 Direction = Vector2.Zero;
        private Vector2 knockbackDirection;
        private float knockbackForce;
        private double knockbackDuration;
        private double knockbackTimeRemaining;

        /// <summary>
        /// The entity list to check when attacking
        /// </summary>
        [CanBeNull]
        protected List<DrawableEntity> EntitiesHitCheckList;

        // stat modifiers
        /// <summary>
        /// The base speed variable for all entities
        /// </summary>
        protected const double SPEED_MAIN = 0.35;

        private const int DODGE_TIME = 250;
        private const int DODGE_INTERVAL = 1000;
        private const int BASE_DODGE_SPEED = 3;

        /// <summary>
        /// When doing some math you might need this
        /// </summary>
        public const float SLOWING_FACTOR = 0.01f;

        private double lastRegenTime;
        private double lastHitTime;

        // Movement events
        public delegate void Movement(Vector2 direction, double speed);

        public event Movement OnMove;

        /// <summary>
        /// A drawable entity
        /// </summary>
        /// <param name="entity">The entity reference</param>
        /// <param name="affiliationType">Entity Affiliation</param>
        /// <param name="showInfo">Will overhead info be shown on screen?</param>
        public DrawableEntity(Entity entity, AffiliationType affiliationType = AffiliationType.None, bool showInfo = true)
        {
            entity.UpdateStats();
            entity.Stats.Restore();
            Entity = entity;
            Affiliation = affiliationType;
            Size = new Vector2(100);
            HitBox = new HitBox(this);
            ColliderBox = new CollisonHitBox(this);
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
                ColliderBox
            };

            if (!showInfo)
            {
                entityBar.HealthProgressBar.Hide();
                entityBar.HealthText.Hide();
                entityBar.EntityLevel.Hide();
                entityBar.StatusEffects.Anchor = Anchor.CentreLeft;
                entityBar.StatusEffects.Origin = Anchor.CentreLeft;
            }

            if (Entity.Weapon != null) Weapon = new DrawableWeapon(this, Affiliation);
            Entity.OnSwapWeapon += setDrawableWeapon;
            entity.OnDamage += delegate(int amount) { addIndicator(amount, DamageType.Damage); };
            entity.OnHeal += delegate(int amount) { addIndicator(amount, DamageType.Heal); };
            entity.OnCrit += delegate(int amount) { addIndicator(amount, DamageType.Crit); };
            entity.OnDamage += delegate { lastHitTime = Clock.CurrentTime; };
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
            entity.OnAddProjectile += parameters =>
            {
                Projectile projectile = new Projectile(parameters);
                projectile.Direction += DirectionLooking;
                QueuedProjectiles.Add(projectile);
            };
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

        public void ApplyKnockback(Vector2 direction, float force, int duration, KnockbackType type)
        {
            knockbackDirection = direction;
            knockbackForce = force;
            knockbackDuration = duration;
            knockbackTimeRemaining = duration;

            switch (type)
            {
                case KnockbackType.None:
                    break;

                case KnockbackType.StopsMovement:
                    Entity.AddEffect(new Stall(duration));
                    break;

                case KnockbackType.Stuns:
                    Entity.AddEffect(new Stun(duration + 300));
                    Weapon.RestWeapon();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void Move(Vector2 direction, double speed)
        {
            float value = (float)(Clock.ElapsedFrameTime * speed);
            ColliderBox.Position += (direction * 0.06f) * value;

            if (!HitBoxScene.Collides(ColliderBox)) OnMove?.Invoke(direction, speed);
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
            if (Weapon.GetWeaponObject().CanAttack) Weapon.Attack((float)angle + 90);
        }

        /// <summary>
        /// Adds an indicator text for when this entity heals/takes damage
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

        public void Dodge()
        {
            if (Entity.CanDodge)
            {
                Entity.CanDodge = false;
                Entity.IsDodging = true;
                Scheduler.AddDelayed(() => { Entity.IsDodging = false; }, DODGE_TIME);
                Scheduler.AddDelayed(() => { Entity.CanDodge = true; }, DODGE_INTERVAL);
                ApplyKnockback(Direction, (int)(BASE_DODGE_SPEED + GetSpeed()), DODGE_TIME, KnockbackType.StopsMovement);
            }
        }

        private void setDrawableWeapon()
        {
            if (Weapon != null)
            {
                RemoveInternal(Weapon, true);
                HitBoxScene.Remove(Weapon.HitBox);
            }

            if (Entity.Weapon != null)
            {
                Weapon = new DrawableWeapon(this, Affiliation);
                Weapon.Affiliation = Affiliation;
                AddInternal(Weapon);
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
        /// <returns>The entity reference for this drawable</returns>
        public Entity GetBase() => Entity;

        /// <summary>
        /// Manages the speed of the entity
        /// </summary>
        /// <returns></returns>
        public double GetSpeed() => (SPEED_MAIN * Entity.Stats.Speed.Current.Value * Entity.SpeedModifier) + Entity.PositionJump;

        protected override void Update()
        {
            // Main update
            base.Update();
            Entity.positionRef = Position;

            // Movement
            Direction = Vector2.Zero;

            if (knockbackTimeRemaining > 0)
            {
                float knockbackDelta = (float)(knockbackTimeRemaining / knockbackDuration);
                Entity.SpeedModifier = knockbackForce * knockbackDelta;
                Direction += knockbackDirection * knockbackForce;

                knockbackTimeRemaining -= Clock.ElapsedFrameTime;

                if (knockbackTimeRemaining < 0)
                {
                    knockbackTimeRemaining = 0;
                    Entity.SpeedModifier = 1;
                }
            }

            // Reset collider box
            ColliderBox.Position = new Vector2(0);

            // Effects logic
            Entity.Affect(Clock.CurrentTime);

            // Skills logic
            Entity.Secondary?.SetPercent(Clock.CurrentTime);
            Entity.Utility?.SetPercent(Clock.CurrentTime);
            Entity.Ultimate?.SetPercent(Clock.CurrentTime);

            // Reset the teleport
            if (Entity.PositionJump > 0) Entity.PositionJump--;

            if (new ElapsedTime(Clock.CurrentTime, lastHitTime) > new Second(0.5))
            {
                Entity.AddTenacity();
                lastHitTime = Clock.CurrentTime;
            }

            // Regen should always be at the bottom
            if (Entity.IsDead || Entity.IsFullHealth) return;

            double elapsedRegenTime = Clock.CurrentTime - lastRegenTime;
            if (Entity.Stats.RegenSpeed.Current.Value == 0) return;

            if (elapsedRegenTime * Entity.Stats.RegenSpeed.Current.Value >= 1000) regen();
        }
    }
}
