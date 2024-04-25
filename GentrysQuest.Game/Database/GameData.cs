using System.Collections.Generic;
using GentrysQuest.Game.Content;
using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Weapon;

namespace GentrysQuest.Game.Database
{
    public static class GameData
    {
        /// <summary>
        /// The game content.
        /// </summary>
        public static ContentManager Content = new ContentManager();

        /// <summary>
        /// The equipped character
        /// </summary>
        public static Character EquipedCharacter { get; private set; }

        /// <summary>
        /// Doubloons
        /// </summary>
        public static Money Money { get; private set; }

        // The lists of entities
        public static List<Character> Characters { get; private set; }
        public static List<Artifact> Artifacts { get; private set; }
        public static List<Weapon> Weapons { get; private set; }

        public static void reset()
        {
            EquipedCharacter = null;
            Money = new Money();
            Characters = new List<Character>();
            Artifacts = new List<Artifact>();
            Weapons = new List<Weapon>();
        }
    }
}
