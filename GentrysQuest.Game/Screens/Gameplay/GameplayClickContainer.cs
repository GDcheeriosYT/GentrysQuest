using GentrysQuest.Game.Entity.Drawables;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osuTK;

namespace GentrysQuest.Game.Screens.Gameplay;

public partial class GameplayClickContainer(DrawablePlayableEntity player) : Container
{
    private bool isHeld;
    private Vector2 mousePos;

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
        isHeld = true;
        return base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseUpEvent e)
    {
        isHeld = false;
        var weapon = player.GetEntityObject().Weapon;
        if (weapon != null) weapon.AttackAmount = 0;
        base.OnMouseUp(e);
    }

    protected override bool OnMouseMove(MouseMoveEvent e)
    {
        mousePos = e.MousePosition;
        return base.OnMouseMove(e);
    }

    protected override void Update()
    {
        base.Update();
        if (isHeld) player.Attack(mousePos);
    }
}
