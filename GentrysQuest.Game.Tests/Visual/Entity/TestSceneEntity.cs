using GentrysQuest.Game.Content.Characters;
using GentrysQuest.Game.Content.Effects;
using GentrysQuest.Game.Content.Enemies;
using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Drawables;
using GentrysQuest.Game.Graphics;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Tests.Visual.Entity
{
    [TestFixture]
    public partial class TestSceneEntity : GentrysQuestTestScene
    {
        private GentrysQuest.Game.Entity.Entity entity;
        private Enemy enemy;
        private DrawableEntity drawableEntity;
        private DrawableEnemyEntity drawableEnemy;
        private SpriteText levelTracker;
        private ProgressBar xpProgress;

        public TestSceneEntity()
        {
            Add(new Box { RelativeSizeAxes = Axes.Both, Colour = Colour4.Gray });

            entity = new TestCharacter(5);
            enemy = new TestEnemy(5);
            Add(levelTracker = new SpriteText
            {
                Text = $"Level: {entity.Experience.Level.Current}"
                       + $"Xp: {entity.Experience.Xp.Current}/{entity.Experience.Xp.Requirement}",
                // Font = FontUsage.Default.With(size: 100),
                Origin = Anchor.BottomLeft,
                Anchor = Anchor.BottomLeft,
                Colour = Colour4.White
            });
            Add(xpProgress = new ProgressBar(0, 100)
            {
                Origin = Anchor.BottomRight,
                Anchor = Anchor.BottomRight,
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(0.2f, 0.02f)
            });
            Add(drawableEntity = new DrawableEntity(entity)
            {
                Position = new Vector2(-200, 0)
            });
            Add(drawableEnemy = new DrawableEnemyEntity(enemy)
            {
                Position = new Vector2(200, 0)
            });

            entity.OnLevelUp += delegate
            {
                levelTracker.Text = $"Level: {entity.Experience.Level.Current}\n"
                                    + $"Xp: {entity.Experience.Xp.Current}/{entity.Experience.Xp.Requirement}";
                xpProgress.Max = entity.Experience.Xp.Requirement.Value;
                xpProgress.Current = entity.Experience.Xp.Current.Value;
                xpProgress.Min = 0;
            };
            entity.OnGainXp += delegate
            {
                levelTracker.Text = $"Level: {entity.Experience.Level.Current}\n"
                                    + $"Xp: {entity.Experience.Xp.Current}/{entity.Experience.Xp.Requirement}";
                xpProgress.Max = entity.Experience.Xp.Requirement.Value;
                xpProgress.Current = entity.Experience.Xp.Current.Value;
                xpProgress.Min = 0;
            };
        }

        [Test]
        public virtual void Start()
        {
            int amount = 100;
            AddStep("Spawn", () =>
            {
                entity.Spawn();
                enemy.Spawn();
            });
            AddStep("Die", () =>
            {
                entity.Die();
                enemy.Die();
            });
            AddSliderStep("Amount", 0, 1000, 100, i => amount = i);
            AddStep("Damage", () =>
            {
                entity.Damage(amount);
                enemy.Damage(amount);
            });
            AddStep("Crit", () =>
            {
                entity.Crit((int)(amount * 1.5));
                enemy.Crit((int)(amount * 1.5));
            });
            AddStep("Heal", () =>
            {
                entity.Heal(amount);
                enemy.Heal(amount);
            });
            AddStep("Burn", () =>
            {
                entity.AddEffect(new Burn());
                enemy.AddEffect(new Burn());
            });
            AddStep("Bleed", () =>
            {
                entity.AddEffect(new Bleed());
                enemy.AddEffect(new Bleed());
            });
            AddStep("LevelUp", () =>
            {
                entity.LevelUp();
                enemy.LevelUp();
            });
            AddStep("AddXp", () =>
            {
                entity.AddXp(amount);
                enemy.AddXp(amount);
            });
        }
    }
}
