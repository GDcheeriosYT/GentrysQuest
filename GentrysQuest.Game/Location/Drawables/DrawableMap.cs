using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace GentrysQuest.Game.Location.Drawables
{
    public sealed partial class DrawableMap : CompositeDrawable
    {
        public Map MapReference { get; }
        public List<DrawableMapObject> mapObjects { get; }

        public DrawableMap(Map map)
        {
            MapReference = map;
            mapObjects = new();

            map.Load();

            foreach (IMapObject mapObject in map.GetObjects())
            {
                DrawableMapObject newMapObject = new DrawableMapObject(mapObject);
                mapObjects.Add(newMapObject);
                AddInternal(newMapObject);
            }

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            Size = new Vector2(5000);
        }
    }
}
