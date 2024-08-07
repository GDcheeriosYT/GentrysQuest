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
        private bool doesDamage;

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
            AlwaysPresent = true;

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

            Disable();
        }

        public void Disable()
        {
            HitBox.Disable();
            Hide();
        }

        public void Enable()
        {
            HitBox.Enable();
            Show();
        }

        public void Disable(int timeMs)
        {
            HitBox.Disable();
            this.FadeOut(timeMs);
            this.ScaleTo(0, timeMs);
        }

        public void Enable(int timeMs)
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
            Enable(100);
            Weapon.AttackAmount += 1;
            AttackPatternCaseHolder caseHolder = Weapon.AttackPattern.GetCase(Weapon.AttackAmount);
            Weapon.Holder.Attack(); // Call the holder base method to handle events.

            if (caseHolder == null)
            {
                Weapon.AttackAmount = 1;
                caseHolder = Weapon.AttackPattern.GetCase(Weapon.AttackAmount);
            }

            var patterns = caseHolder.GetEvents();
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
                    Weapon.Damage.Add(Weapon.Damage.GetPercentFromTotal(pattern.DamagePercent));
                    Weapon.Holder.SpeedModifier = pattern.MovementSpeed;
                    onHitEffect = pattern.OnHitEffect;
                    doesDamage = pattern.DoesDamage;

                    if (!HitBox.Enabled) return;

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
                Disable(100);
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
                    if (!DamageQueue.Check(hitbox) && Weapon.IsGeneralDamageMode && hitbox.GetType() != typeof(CollisonHitBox) && doesDamage)
                    {
                        DamageDetails details = new DamageDetails();
                        DrawableEntity receiver = null;
                        Entity receiverBase = null;
                        bool isValid = true;
                        bool isCrit = false;

                        switch (hitbox.GetParent())
                        {
                            case DrawableEntity drawableEntity:
                                receiver = drawableEntity;
                                receiverBase = receiver.GetEntityObject();
                                break;

                            default:
                                isValid = false;
                                break;
                        }

                        if (!isValid) continue;

                        int damage = (int)(Weapon.Damage.Current.Value + Weapon.Holder.Stats.Attack.Current.Value);

                        if (Weapon.Holder.Stats.CritRate.Current.Value > MathBase.RandomInt(0, 100))
                        {
                            isCrit = true;
                            damage += (int)MathBase.GetPercent(damage,
                                Weapon.Holder.Stats.CritDamage.Current.Value
                            );
                            details.IsCrit = true;
                            receiverBase.CritWithDefense(damage);
                        }
                        else
                        {
                            receiverBase.DamageWithDefense(damage);
                        }

                        receiverBase.RemoveTenacity();
                        details.Damage = damage;
                        details.Receiver = receiverBase;
                        details.Sender = Weapon.Holder;

                        Vector2 direction = MathBase.GetDirection(Weapon.Holder.positionRef, receiver.Position);
                        float knockbackForce = (float)(1 + Weapon.Damage.GetDefault() / 100);
                        if (isCrit) knockbackForce *= 1.5f;
                        if (receiverBase.HasTenacity()) receiver.ApplyKnockback(direction, 0.54f, 100, KnockbackType.StopsMovement);
                        else receiver.ApplyKnockback(direction, knockbackForce, (int)knockbackForce * 200, KnockbackType.Stuns);

                        if (!details.Sender.EnemyHitCounter.TryAdd(details.Receiver, 1)) details.Sender.EnemyHitCounter[details.Receiver]++;

                        receiverBase.OnHit(details);
                        if (onHitEffect != null && onHitEffect.Applies()) receiverBase.AddEffect(onHitEffect.Effect);
                        Weapon.HitEntity(details);

                        switch (receiverBase)
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
                                if (receiverBase.IsDead)
                                {
                                    GameData.CurrentStats.AddToStat(StatTypes.Hits);
                                    if (isCrit) GameData.CurrentStats.AddToStat(StatTypes.Crits);
                                    GameData.CurrentStats.AddToStat(StatTypes.Damage, damage);
                                    GameData.CurrentStats.AddToStat(StatTypes.MostDamage, damage);
                                    int money = receiverBase.GetMoneyReward();
                                    GameData.CurrentStats.AddToStat(StatTypes.MoneyGained, money);
                                    GameData.CurrentStats.AddToStat(StatTypes.MoneyGainedOnce, money);
                                    Weapon.Holder.AddXp(receiverBase.GetXpReward());
                                    GameData.Money.Hand(money);

                                    Weapon.Weapon reward = receiverBase.GetWeaponReward();
                                    if (reward != null) GameData.Add(reward);

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
                Weapon.UpdateStats();
                Weapon.Holder.SpeedModifier = 1;
            }
        }
    }
}
