using System;
using osu.Framework.Input.Events;
using osuTK.Input;

namespace GentrysQuest.Game.Entity.Drawables;

public partial class DrawablePlayableEntity : DrawableEntity
{
    public DrawablePlayableEntity(Character entity)
        : base(entity)
    {
        // pass
    }

    protected override bool OnClick(ClickEvent e)
    {
        Console.WriteLine(e.MousePosition);
        return base.OnClick(e);
    }

    protected override bool OnKeyDown(KeyDownEvent e)
    {
        switch (e.Key)
        {
            case Key.A:
                X -= (float)(Clock.ElapsedFrameTime * 3);
                break;

            case Key.D:
                X += (float)(Clock.ElapsedFrameTime * 3);
                break;

            case Key.W:
                Y -= (float)(Clock.ElapsedFrameTime * 3);
                break;

            case Key.S:
                Y += (float)(Clock.ElapsedFrameTime * 3);
                break;
        }

        return base.OnKeyDown(e);
    }
}
