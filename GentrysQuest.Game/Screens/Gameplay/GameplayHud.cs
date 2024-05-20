using GentrysQuest.Game.Entity;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace GentrysQuest.Game.Screens.Gameplay
{
    public partial class GameplayHud : CompositeDrawable
    {
        private Entity.Entity entityTracker;

        private Container barsContainer;

        private readonly GameplayBar healthBar;
        private readonly GameplayBar experienceBar;

        private SpriteText levelText;

        public GameplayHud()
        {
            RelativeSizeAxes = Axes.Both;
            Depth = -2;

            AddInternal(
                barsContainer = new Container()
                {
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,

                    CornerRadius = 4,
                    CornerExponent = 2,
                    Masking = true,

                    Size = new Vector2(0.4f, 0.15f),
                    Position = new Vector2(0.05f, -0.1f),

                    Children = new Drawable[]
                    {
                        healthBar = new GameplayBar
                        {
                            RelativeSizeAxes = Axes.Both,
                            RelativePositionAxes = Axes.Both,
                            Anchor = Anchor.TopLeft,
                            Origin = Anchor.TopLeft,
                            Size = new Vector2(1, 0.5f),
                            ForegroundColour = Colour4.LimeGreen
                        },
                        experienceBar = new GameplayBar
                        {
                            RelativeSizeAxes = Axes.Both,
                            RelativePositionAxes = Axes.Both,
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            Size = new Vector2(1, 0.5f)
                        },
                        levelText = new SpriteText
                        {
                            Text = "Level 0",
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.TopLeft,
                            RelativePositionAxes = Axes.Both,
                            Font = FontUsage.Default.With(size: 20),
                            Position = new Vector2(0)
                        }
                    }
                });
        }

        public void SetEntity(Entity.Entity theEntity)
        {
            entityTracker = theEntity;
            SetHealth(theEntity.Stats.Health);
            SetExperience(theEntity.Experience);
            entityTracker.Stats.Health.Current.ValueChanged += delegate { SetHealth(theEntity.Stats.Health); };
            entityTracker.Experience.Level.Current.ValueChanged += delegate { SetExperience(theEntity.Experience); };
            entityTracker.Experience.Xp.Current.ValueChanged += delegate { SetExperience(theEntity.Experience); };
            entityTracker.OnUpdateStats += delegate
            {
                SetHealth(theEntity.Stats.Health);
                SetExperience(theEntity.Experience);
            };
        }

        public void SetHealth(Stat health)
        {
            healthBar.Current = (int)health.Current.Value;
            healthBar.Max = (int)health.Total();
        }

        public void SetExperience(Experience experience)
        {
            levelText.Text = $"Level {experience.Level.Current}";
            experienceBar.Current = experience.Xp.Current.Value;
            experienceBar.Max = experience.Xp.Requirement.Value;
        }
    }
}
