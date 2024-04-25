using System.Collections.Generic;
using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Location
{
    public interface IMap
    {
        /// <summary>
        /// The map name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The list of enemies the map will use
        /// </summary>
        List<Enemy> Enemies { get; }

        /// <summary>
        /// The list of families the map will use
        /// </summary>
        List<Family> Families { get; }

        /// <summary>
        /// Objects in the map
        /// </summary>
        List<IMapObject> mapObjects { get; }

        /// <summary>
        /// Loads the map
        /// </summary>
        void Load();

        /// <summary>
        /// Gets the objects in the map
        /// </summary>
        /// <returns>The map objects</returns>
        List<IMapObject> GetObjects();
    }
}
