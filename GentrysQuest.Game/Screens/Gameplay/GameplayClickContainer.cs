using GentrysQuest.Game.Entity.Drawables;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osuTK;

namespace GentrysQuest.Game.Screens.Gameplay;

public partial class GameplayClickContainer : Container
{
    private DrawablePlayableEntity player;

    public GameplayClickContainer(DrawablePlayableEntity player)
    {
        this.player = player;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        RelativeSizeAxes = Axes.Both;
        RelativePositionAxes = Axes.Both;
        Size = new Vector2(20f);
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
    }

    protected override bool OnMouseDown(MouseDownEvent e)
    {
        Logger.Log("" + e.MousePosition);
        player.Attack(e.MousePosition);
        return base.OnMouseDown(e);
    }
}
