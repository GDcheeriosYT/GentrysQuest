using System;
using System.Collections.Generic;
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
    public partial class DrawableWeapon : CompositeDrawable
    {
        protected readonly Weapon.Weapon Weapon;
        protected readonly Sprite Sprite;
        protected readonly HitBox WeaponHitBox;
        protected readonly DamageQueue DamageQueue = new();
        public AffiliationType Affiliation;
        protected float Distance;

        public DrawableWeapon(Weapon.Weapon weapon, AffiliationType affiliation)
        {
            Weapon = weapon;
            Affiliation = affiliation;
            WeaponHitBox = new HitBox(this);
            Size = new Vector2(1f);
            RelativeSizeAxes = Axes.Both;
            Colour = Colour4.White;
            Anchor = Anchor.Centre;
            Origin = weapon.Origin;
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

        private void disable(int timeMs)
        {
            WeaponHitBox.Disable();
            this.FadeOut(timeMs);
            this.ScaleTo(0, timeMs);
        }

        private void enable(int timeMs)
        {
            WeaponHitBox.Enable();
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
                    if (pattern.Direction != null) this.RotateTo((float)pattern.Direction + direction, duration: speed, pattern.Transition);
                    if (pattern.Position != null) this.MoveTo((Vector2)pattern.Position, duration: speed, pattern.Transition);
                    if (pattern.Size != null) this.ResizeTo((Vector2)pattern.Size, duration: speed, pattern.Transition);
                    if (pattern.HitboxSize != null) this.WeaponHitBox.ScaleTo((Vector2)pattern.HitboxSize, duration: speed, pattern.Transition);
                    if (pattern.Distance != null) Distance = (float)pattern.Distance;
                    if (pattern.ResetHitBox) DamageQueue.Clear();
                    Weapon.Damage.SetAdditional(Weapon.Damage.GetPercentFromDefault(pattern.DamagePercent));
                    if (pattern.MovementSpeed != null) Weapon.Holder.SpeedModifier = (float)pattern.MovementSpeed;
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
                Position = MathBase.GetAngleToVector(Rotation - 90) * Distance;

                foreach (var hitbox in HitBoxScene.GetIntersections(WeaponHitBox))
                {
                    if (!DamageQueue.Check(hitbox))
                    {
                        Entity entity;
                        bool isCrit = false;

                        try
                        {
                            entity = hitbox.getParent().GetEntityObject();
                        }
                        catch (Exception e)
                        {
                            entity = new Entity();
                        }

                        int damage = (int)(Weapon.Damage.Current.Value + Weapon.Holder.Stats.Attack.Current.Value);
                        damage -= (int)entity.Stats.Defense.Current.Value;

                        if (Weapon.Holder.Stats.CritRate.Current.Value > MathBase.RandomInt(0, 100))
                        {
                            isCrit = true;
                            damage += (int)MathBase.GetPercent(
                                Weapon.Holder.Stats.Attack.Current.Value,
                                Weapon.Holder.Stats.CritDamage.Current.Value
                            );
                            entity.Crit(damage);
                        }
                        else entity.Damage(damage);

                        if (entity.IsDead) Weapon.Holder.AddXp(entity.GetXpReward());

                        bool isWeapon = true; // weapon hitboxes are tracked...

                        switch (entity)
                        {
                            case Character character:
                                GameData.CurrentStats.AddToStat(StatTypes.HitsTaken);
                                if (isCrit) GameData.CurrentStats.AddToStat(StatTypes.CritsTaken);
                                isWeapon = false;
                                break;

                            case Enemy enemy:
                                isWeapon = false;
                                break;
                        }

                        switch (Weapon.Holder)
                        {
                            case Character character:
                                if (isWeapon) return;

                                if (entity.IsDead)
                                {
                                    GameData.CurrentStats.AddToStat(StatTypes.Hits);
                                    if (isCrit) GameData.CurrentStats.AddToStat(StatTypes.Crits);
                                    GameData.CurrentStats.AddToStat(StatTypes.Damage, damage);
                                    GameData.CurrentStats.AddToStat(StatTypes.MostDamage, damage);
                                    int money = entity.GetMoneyReward();
                                    GameData.CurrentStats.AddToStat(StatTypes.MoneyGained, money);
                                    GameData.CurrentStats.AddToStat(StatTypes.MoneyGainedOnce, money);
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
