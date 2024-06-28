using System.Collections.Generic;
using GentrysQuest.Game.Entity;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace GentrysQuest.Game.Location.Drawables
{
    public sealed partial class DrawableMap : CompositeDrawable
    {
        public Map MapReference { get; private set; }
        public List<DrawableMapObject> mapObjects { get; private set; }

        public DrawableMap()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            Size = new Vector2(5000);
        }

        public void Load(Map map)
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
        }

        public void Unload()
        {
            foreach (DrawableMapObject mapObject in mapObjects)
            {
                HitBoxScene.Remove(mapObject.Collider);
                RemoveInternal(mapObject, true);
            }
        }
    }
}
