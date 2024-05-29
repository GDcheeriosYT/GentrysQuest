using GentrysQuest.Game.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace GentrysQuest.Game.Screens.Gameplay;

public partial class GameplayBar : ProgressBar
{
    private readonly SpriteText barText;

    public GameplayBar()
        : base(0, 1)
    {
        AddInternal(
            barText = new SpriteText
            {
                Text = $"{Min}/{Max}",
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                Font = FontUsage.Default.With(size: 45)
            });

        OnProgressChange += delegate { barText.Text = $"{Current}/{Max}"; };
    }
}
