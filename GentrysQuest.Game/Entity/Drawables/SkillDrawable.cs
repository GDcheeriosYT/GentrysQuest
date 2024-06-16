using GentrysQuest.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace GentrysQuest.Game.Entity.Drawables
{
    public partial class SkillDrawable : CompositeDrawable
    {
        private readonly Skill skillReference;
        private readonly SpriteText skillName;
        private readonly ProgressBar percentageDisplay;
        private readonly Sprite skillDisplay;

        public SkillDrawable(Skill skillReference)
        {
            if (skillReference == null)
            {
                Hide();
            }
            else
            {
                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;
                Size = new Vector2(100);
                this.skillReference = skillReference;

                InternalChildren = new Drawable[]
                {
                    new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Children = new Drawable[]
                        {
                            new FillFlowContainer
                            {
                                Direction = FillDirection.Vertical,
                                AutoSizeAxes = Axes.Both,
                                Anchor = Anchor.TopCentre,
                                Children = new Drawable[]
                                {
                                    new Container
                                    {
                                        Masking = true,
                                        CornerExponent = 2,
                                        CornerRadius = 6,
                                        Origin = Anchor.Centre,
                                        Size = new Vector2(60, 10),
                                        Child = percentageDisplay = new ProgressBar(0, 100)
                                        {
                                            RelativeSizeAxes = Axes.Both,
                                            BackgroundColour = new Colour4(0, 0, 0, 0),
                                            ForegroundColour = new Colour4(255, 255, 255, 200),
                                            Current = 0
                                        }
                                    },
                                    skillDisplay = new Sprite
                                    {
                                        Size = new Vector2(64),
                                        Origin = Anchor.Centre,
                                        Margin = new MarginPadding(2)
                                    },
                                    skillName = new SpriteText
                                    {
                                        Text = skillReference.Name,
                                        Origin = Anchor.Centre
                                    }
                                }
                            }
                        }
                    }
                };
            }
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textureStore)
        {
            if (skillDisplay != null) skillDisplay.Texture = textureStore.Get(skillReference.TextureMapping.Get("Icon"));
        }

        protected override void Update()
        {
            base.Update();

            percentageDisplay.Current = skillReference.PercentToDone;
        }
    }
}
