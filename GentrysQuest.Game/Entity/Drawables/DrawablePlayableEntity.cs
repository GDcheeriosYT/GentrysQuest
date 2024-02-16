using System;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osuTK.Input;

namespace GentrysQuest.Game.Entity.Drawables;

public partial class DrawablePlayableEntity : DrawableEntity
{
    public DrawablePlayableEntity(Character entity)
        : base(entity, false)
    {
        // pass
    }

    /// <summary>
    /// The move event
    /// </summary>
    /// <param name="isHorizontal">If it's horizontal target</param>
    /// <param name="negative">If the value's going down or not</param>
    private void move(bool isHorizontal, bool negative)
    {
        var speed = Entity.Stats.Speed.CurrentValue;
        var value = (float)(Clock.ElapsedFrameTime * speed);
        var duration = 0;

        if (negative) value = -value;

        if (isHorizontal)
        {
            this.MoveToX(
                X + value,
                duration
            );
        }
        else
        {
            this.MoveToY(
                Y + value,
                duration
            );
        }
    }

    protected override bool OnClick(ClickEvent e)
    {
        Console.WriteLine(e.MousePosition);
        return base.OnClick(e);
    }

    protected override void Update()
    {
        base.Update();

        if (Keyboard.GetState().IsKeyDown(Key.A))
        {
            move(true, true);
        }

        if (Keyboard.GetState().IsKeyDown(Key.D))
        {
            move(true, false);
        }

        if (Keyboard.GetState().IsKeyDown(Key.W))
        {
            move(false, true);
        }

        if (Keyboard.GetState().IsKeyDown(Key.S))
        {
            move(false, false);
        }
    }
}
