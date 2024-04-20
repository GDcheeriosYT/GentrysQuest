using System.Collections.Generic;

namespace GentrysQuest.Game.Location
{
    public interface IMap
    {
        string Name { get; }

        /// <summary>
        /// Objects in the map
        /// </summary>
        List<IMapObject> mapObjects { get; }

        /// <summary>
        /// Loads the map
        /// </summary>
        void Load();

        List<IMapObject> GetObjects();
    }
}
