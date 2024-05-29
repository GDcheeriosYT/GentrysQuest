using System;
using osu.Framework.Graphics;
using osuTK;

namespace GentrysQuest.Game.Location;

public partial class MapObject(bool hasCollider, Vector2 size, Vector2 position, Colour4 colour)
    : IMapObject
{
    public bool HasCollider { get; } = hasCollider;
    public Vector2 Size { get; } = size;
    public Vector2 Position { get; } = position;
    public Colour4 Colour { get; } = colour;
    public event EventHandler onTouch;

    public void OnTouch()
    {
        onTouch?.Invoke(null, null);
    }
}
