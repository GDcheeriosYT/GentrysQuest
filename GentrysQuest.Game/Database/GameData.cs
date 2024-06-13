using System.Collections.Generic;
using GentrysQuest.Game.Content;
using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Weapon;
using GentrysQuest.Game.Overlays.Notifications;

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

        /// <summary>
        /// Game stats
        /// </summary>
        public static Statistics Statistics { get; private set; }

        public static StatTracker CurrentStats { get; private set; }

        // The lists of entities
        public static List<Character> Characters { get; private set; }
        public static List<Artifact> Artifacts { get; private set; }
        public static List<Weapon> Weapons { get; private set; }

        public static void Reset()
        {
            EquipedCharacter = null;
            Money = new Money();
            Statistics = new Statistics();
            CurrentStats = new StatTracker();
            Characters = new List<Character>();
            Artifacts = new List<Artifact>();
            Weapons = new List<Weapon>();
        }

        public static void StartStatTracker() => CurrentStats = new StatTracker();

        public static void WrapUpStats()
        {
            Statistics.Totals.Merge(CurrentStats);
            Statistics.Most = Statistics.Most.GetBest(CurrentStats);
        }

        /// <summary>
        /// Calls the AddNotification method.
        /// If you don't want a notification don't call this method.
        /// </summary>
        /// <param name="entity">entity to add</param>
        public static void Add(EntityBase entity)
        {
            NotificationContainer.Instance.AddNotification(new Notification($"Obtained {entity.StarRating.Value} star {entity.Name}", NotificationType.Obtained));

            switch (entity)
            {
                case Character character:
                    Characters.Add(character);
                    break;

                case Artifact artifact:
                    Artifacts.Add(artifact);
                    break;

                case Weapon weapon:
                    Weapons.Add(weapon);
                    break;
            }
        }

        public static void EquipCharacter(Character character)
        {
            NotificationContainer.Instance.AddNotification(new Notification($"Equipped {character.Name}", NotificationType.Informative));
            EquipedCharacter = character;
        }
    }
}
