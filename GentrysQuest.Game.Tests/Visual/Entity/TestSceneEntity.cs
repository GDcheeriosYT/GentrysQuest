using GentrysQuest.Game.Content.Characters;
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
        private DrawableEntity drawableEntity;
        private SpriteText levelTracker;
        private ProgressBar xpProgress;

        public TestSceneEntity()
        {
            Add(new Box { RelativeSizeAxes = Axes.Both, Colour = Colour4.Gray });

            entity = new TestCharacter(5);
            Add(levelTracker = new SpriteText
            {
                Text = $"Level: {entity.Experience.Level.current}"
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
            Add(drawableEntity = new DrawableEntity(entity));

            entity.OnLevelUp += delegate
            {
                levelTracker.Text = $"Level: {entity.Experience.Level.current}\n"
                                    + $"Xp: {entity.Experience.Xp.Current}/{entity.Experience.Xp.Requirement}";
                xpProgress.Max = entity.Experience.Xp.Requirement;
                xpProgress.Current = entity.Experience.Xp.Current;
                xpProgress.Min = 0;
            };
            entity.OnGainXp += delegate
            {
                levelTracker.Text = $"Level: {entity.Experience.Level.current}\n"
                                    + $"Xp: {entity.Experience.Xp.Current}/{entity.Experience.Xp.Requirement}";
                xpProgress.Max = entity.Experience.Xp.Requirement;
                xpProgress.Current = entity.Experience.Xp.Current;
                xpProgress.Min = 0;
            };
        }

        [Test]
        public virtual void Start()
        {
            int amount = 100;
            AddStep("Spawn", () => entity.Spawn());
            AddStep("Die", () => entity.Die());
            AddSliderStep("Amount", 0, 1000, 100, i => amount = i);
            AddStep("Damage", () => entity.Damage(amount));
            AddStep("Crit", () => entity.Crit((int)(amount * 1.5)));
            AddStep("Heal", () => entity.Heal(amount));
            AddStep("LevelUp", () => entity.LevelUp());
            AddStep("AddXp", () => entity.AddXp(amount));
        }
    }
}
