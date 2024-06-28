using System.Linq;
using GentrysQuest.Game.Database;
using GentrysQuest.Game.Entity.Weapon;
using GentrysQuest.Game.Graphics.TextStyles;
using GentrysQuest.Game.Location.Drawables;
using GentrysQuest.Game.Utils;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class Projectile : CompositeDrawable
    {
        /// <summary>
        /// Design of the projectile
        /// </summary>
        public CustomSprite Design;

        /// <summary>
        /// Speed of the projectile
        /// </summary>
        public double Speed;

        /// <summary>
        /// Direction of the projectile
        /// </summary>
        public double Direction;

        /// <summary>
        /// How long the projectile will last
        /// </summary>
        public double Lifetime;

        /// <summary>
        /// Hitbox of the projectile
        /// </summary>
        public HitBox HitBox;

        /// <summary>
        /// The amount of times the projectile can pass through enemies
        /// </summary>
        public int PassthroughAmount;

        /// <summary>
        /// projectile damage
        /// </summary>
        public int Damage;

        /// <summary>
        /// How it affects
        /// </summary>
        public OnHitEffect OnHitEffect;

        /// <summary>
        /// The amount of hits that the projectile has hit
        /// </summary>
        private int hits;

        /// <summary>
        /// track whether the projectile has been started or not.
        /// </summary>
        private bool started;

        /// <summary>
        /// If the damage takes defense into account
        /// </summary>
        public bool TakesDefense;

        /// <summary>
        /// If the damage takes weapon damage into account
        /// </summary>
        public bool TakesNormalDamage;

        /// <summary>
        /// If the damage takes holder damage into account
        /// </summary>
        public bool TakesHolderDamage;

        private DrawableEntity shooter;

        private readonly DamageQueue damageQueue = new DamageQueue();

        public AffiliationType Affiliation;

        public Projectile(ProjectileParameters parameters)
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            InternalChild = new Box
            {
                Size = new Vector2(16),
                Colour = Colour4.Black
            };
            Speed = parameters.Speed;
            Direction = parameters.Direction;
            Lifetime = parameters.Lifetime;
            HitBox = parameters.HitBox;
            PassthroughAmount = parameters.PassthroughAmount;
            Damage = parameters.Damage;
            OnHitEffect = parameters.OnHitEffect;
            TakesDefense = parameters.TakesDefense;
            TakesNormalDamage = parameters.TakesNormalDamage;
            TakesHolderDamage = parameters.TakesHolderDamage;
        }

        /// <summary>
        /// This sets up the projectile for shooting.
        /// Sets the affiliation and which then sets up the hitbox.
        /// </summary>
        /// <param name="shooter">the shooter</param>
        /// <param name="time">the current time</param>
        public void ShootFrom(DrawableEntity shooter)
        {
            this.shooter = shooter;
            hits = 0;
            Position = shooter.Position;
            Affiliation = shooter.Affiliation;
            AddInternal(HitBox = new HitBox(this));
            started = true;
            if (TakesHolderDamage) Damage += (int)shooter.GetEntityObject().Stats.Attack.Current.Value;
        }

        protected override void Update()
        {
            base.Update();

            if (!started) return;

            Position += (MathBase.GetAngleToVector(Direction) * 0.0005f) * (float)(Speed * Clock.ElapsedFrameTime);

            foreach (var hitBox in HitBoxScene.GetIntersections(HitBox).Where(hitBox => !damageQueue.Check(hitBox)))
            {
                DamageDetails details = new();
                Entity entity;
                bool isValid = true;

                switch (hitBox.GetParent())
                {
                    case DrawableEntity drawableEntity:
                        entity = drawableEntity.GetEntityObject();
                        break;

                    case DrawableMapObject mapObject:
                        entity = new Entity();
                        isValid = false;
                        Hide();
                        HitBox.Disable();
                        break;

                    default:
                        isValid = false;
                        entity = new Entity();
                        break;
                }

                if (!isValid) continue;

                hits++;
                int damage = Damage;
                if (TakesDefense) entity.DamageWithDefense(damage);
                entity.Damage(damage);

                details.Damage = damage;
                details.Receiver = entity;
                details.Sender = shooter.GetEntityObject();

                entity.OnHit(details);

                if (OnHitEffect != null && OnHitEffect.Applies()) entity.AddEffect(OnHitEffect.Effect);

                switch (entity)
                {
                    case Character character:
                        GameData.CurrentStats.AddToStat(StatTypes.HitsTaken);
                        GameData.CurrentStats.AddToStat(StatTypes.DamageTaken, damage);
                        GameData.CurrentStats.AddToStat(StatTypes.MostDamageTaken, damage);
                        if (character.IsDead) GameData.CurrentStats.AddToStat(StatTypes.Deaths);
                        break;

                    case Enemy enemy:
                        GameData.CurrentStats.AddToStat(StatTypes.Hits);
                        GameData.CurrentStats.AddToStat(StatTypes.Damage, damage);
                        GameData.CurrentStats.AddToStat(StatTypes.MostDamage, damage);

                        if (entity.IsDead)
                        {
                            GameData.CurrentStats.AddToStat(StatTypes.Kills);
                            int money = enemy.GetMoneyReward();
                            GameData.CurrentStats.AddToStat(StatTypes.MoneyGained, money);
                            GameData.CurrentStats.AddToStat(StatTypes.MoneyGainedOnce, money);
                            details.Sender.AddXp(enemy.GetXpReward());
                            GameData.Money.Hand(money);
                        }

                        break;
                }

                damageQueue.Add(hitBox);
            }

            if (hits < PassthroughAmount) return;

            Hide();
        }
    }
}
