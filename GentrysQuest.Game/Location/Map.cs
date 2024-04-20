using System.Collections.Generic;

namespace GentrysQuest.Game.Location
{
    public class Map : IMap
    {
        public string Name { get; protected set; }
        public List<IMapObject> mapObjects { get; } = new();

        public virtual void Load()
        {
            // todo: Write some method to load from map file
        }

        public List<IMapObject> GetObjects() => mapObjects;
    }
}
