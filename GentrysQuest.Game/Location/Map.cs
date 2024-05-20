using System.Collections.Generic;
using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Location
{
    public class Map : IMap
    {
        public string Name { get; protected set; }
        public List<Enemy> Enemies { get; } = new();
        public List<Family> Families { get; } = new();
        public List<IMapObject> mapObjects { get; } = new();

        public virtual void Load()
        {
            // todo: Write some method to load from map file
        }

        public List<IMapObject> GetObjects() => mapObjects;
        public int Difficulty { get; protected set; } = 0;
        public bool DifficultyScales { get; protected set; } = false;
    }
}
