using System;
using osu.Framework.Graphics;
using osuTK;

namespace GentrysQuest.Game.Location
{
    public interface IMapObject
    {
        /// <summary>
        /// If entities can collide with this object
        /// </summary>
        bool HasCollider { get; }

        /// <summary>
        /// The Size of the object
        /// </summary>
        Vector2 Size { get; }

        /// <summary>
        /// The position of the object
        /// </summary>
        Vector2 Position { get; }

        /// <summary>
        /// The colour of the object
        /// </summary>
        Colour4 Colour { get; }

        /// <summary>
        /// How tought the object is
        /// </summary>
        int Toughness { get; }

        /// <summary>
        /// The method that happens when this object is touched
        /// </summary>
        event EventHandler onTouch;
    }
}
