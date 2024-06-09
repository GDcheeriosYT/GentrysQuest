using System;
using System.Collections.Generic;
using System.Linq;
using GentrysQuest.Game.Database;
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
    public partial class DrawableWeapon : CompositeDrawable, IDrawableEntity
    {
        protected readonly Weapon.Weapon Weapon;
        protected readonly DrawableEntity Holder;
        protected readonly DamageQueue DamageQueue = new();
        public Sprite Sprite { get; set; }
        public HitBox HitBox { get; set; }
        public AffiliationType Affiliation { get; set; }
        public float Distance;
        public Vector2 PositionHolder;
        private OnHitEffect onHitEffect;

        public DrawableWeapon(DrawableEntity entity, AffiliationType affiliation)
        {
            Holder = entity;
            Weapon = entity.GetEntityObject().Weapon;
            Affiliation = affiliation;
            HitBox = new HitBox(this);
            Size = new Vector2(1f);
            RelativeSizeAxes = Axes.Both;
            Colour = Colour4.White;
            Anchor = Anchor.Centre;

            if (Weapon != null)
            {
                Origin = Weapon.Origin;
                InternalChildren = new Drawable[]
                {
                    Sprite = new Sprite
                    {
                        RelativeSizeAxes = Axes.Both
                    },
                    HitBox
                };
                Weapon.CanAttack = true;
            }

            disable();
        }

        private void disable()
        {
            HitBox.Disable();
            Hide();
        }

        private void enable()
        {
            HitBox.Enable();
            Show();
        }

        private void disable(int timeMs)
        {
            HitBox.Disable();
            this.FadeOut(timeMs);
            this.ScaleTo(0, timeMs);
        }

        private void enable(int timeMs)
        {
            HitBox.Enable();
            this.FadeIn(timeMs);
            this.ScaleTo(1, timeMs);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            Sprite.Colour = Colour4.White;
            Sprite.Texture = textures.Get(Weapon.TextureMapping.Get("Base"));
        }

        public Weapon.Weapon GetWeaponObject() { return Weapon; }

        public void Attack(float direction)
        {
            DamageQueue.Clear();
            Weapon.CanAttack = false;
            enable(100);
            Weapon.AttackAmount += 1;
            AttackPatternCaseHolder caseHolder = Weapon.AttackPattern.GetCase(Weapon.AttackAmount);
            Weapon.Holder.Attack(); // Call the holder base method to handle events.
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
                double speed = pattern.TimeMs / Weapon.Holder.Stats.AttackSpeed.Current.Value;
                Scheduler.AddDelayed(() =>
                {
                    this.RotateTo(pattern.Direction + direction, duration: speed, pattern.Transition);
                    this.TransformTo(nameof(PositionHolder), pattern.Position, speed, pattern.Transition);
                    this.ResizeTo(pattern.Size, duration: speed, pattern.Transition);
                    HitBox.ScaleTo(pattern.HitboxSize, duration: speed, pattern.Transition);
                    this.TransformTo(nameof(Distance), pattern.Distance, speed, pattern.Transition);
                    if (pattern.ResetHitBox) DamageQueue.Clear();
                    Weapon.Damage.SetAdditional(Weapon.Damage.GetPercentFromDefault(pattern.DamagePercent));
                    Weapon.Holder.SpeedModifier = pattern.MovementSpeed;
                    onHitEffect = pattern.OnHitEffect;

                    if (pattern.Projectiles == null) return;

                    foreach (var projectile in pattern.Projectiles.Select(parameters => new Projectile(parameters)))
                    {
                        projectile.Position *= Distance;
                        projectile.Direction += direction - 90;
                        Holder.QueuedProjectiles.Add(projectile);
                    }
                }, delay);
                delay += speed;
            }

            Scheduler.AddDelayed(() => // Add delay to enable weapon attacking
            {
                disable(100);
            }, delay + 50);

            Scheduler.AddDelayed((() =>
            {
                Weapon.CanAttack = true;
            }), delay + 110);
        }

        protected override void Update()
        {
            base.Update();

            if (!Weapon.CanAttack)
            {
                Position = MathBase.RotateVector(PositionHolder, Rotation - 180) + MathBase.GetAngleToVector(Rotation - 90) * Distance;

                foreach (var hitbox in HitBoxScene.GetIntersections(HitBox))
                {
                    if (!DamageQueue.Check(hitbox) && Weapon.IsGeneralDamageMode)
                    {
                        DamageDetails details = new DamageDetails();
                        Entity entity;
                        bool isValid = true;
                        bool isCrit = false;

                        try
                        {
                            entity = hitbox.getParent().GetEntityObject();
                        }
                        catch (Exception e)
                        {
                            isValid = false;
                            entity = new Entity();
                        }

                        if (!isValid) continue;

                        int damage = (int)(Weapon.Damage.Current.Value + Weapon.Holder.Stats.Attack.Current.Value);
                        damage -= (int)(entity.Stats.Defense.Current.Value * entity.DefenseModifier);

                        if (Weapon.Holder.Stats.CritRate.Current.Value > MathBase.RandomInt(0, 100))
                        {
                            isCrit = true;
                            damage += (int)MathBase.GetPercent(
                                Weapon.Holder.Stats.Attack.Current.Value,
                                Weapon.Holder.Stats.CritDamage.Current.Value
                            );
                            details.IsCrit = true;
                            entity.Crit(damage);
                        }
                        else
                        {
                            entity.Damage(damage);
                        }

                        details.Damage = damage;
                        details.Receiver = entity;
                        details.Sender = Weapon.Holder;

                        entity.OnHit(details);
                        if (onHitEffect != null && onHitEffect.Applies()) entity.AddEffect(onHitEffect.Effect);
                        Weapon.HitEntity(details);

                        switch (entity)
                        {
                            case Character character:
                                GameData.CurrentStats.AddToStat(StatTypes.HitsTaken);
                                if (isCrit) GameData.CurrentStats.AddToStat(StatTypes.CritsTaken);
                                break;

                            case Entity:
                                GameData.CurrentStats.AddToStat(StatTypes.Hits);
                                break;
                        }

                        switch (Weapon.Holder)
                        {
                            case Character character:
                                if (entity.IsDead)
                                {
                                    GameData.CurrentStats.AddToStat(StatTypes.Hits);
                                    if (isCrit) GameData.CurrentStats.AddToStat(StatTypes.Crits);
                                    GameData.CurrentStats.AddToStat(StatTypes.Damage, damage);
                                    GameData.CurrentStats.AddToStat(StatTypes.MostDamage, damage);
                                    int money = entity.GetMoneyReward();
                                    GameData.CurrentStats.AddToStat(StatTypes.MoneyGained, money);
                                    GameData.CurrentStats.AddToStat(StatTypes.MoneyGainedOnce, money);
                                    Weapon.Holder.AddXp(entity.GetXpReward());
                                    GameData.Money.Hand(money);
                                    GameData.Weapons.Add(entity.GetWeaponReward());
                                    GameData.CurrentStats.AddToStat(StatTypes.Kills);
                                }

                                break;
                        }

                        DamageQueue.Add(hitbox);
                    }
                }
            }

            else
            {
                Weapon.Damage.SetAdditional(0);
                Weapon.Holder.SpeedModifier = 1;
            }
        }
    }
}
