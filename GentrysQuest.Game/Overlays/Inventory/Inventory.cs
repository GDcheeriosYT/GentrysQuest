using System.Collections.Generic;
using GentrysQuest.Game.Entity;

namespace GentrysQuest.Game.Overlays.Inventory
{
    public class Inventory
    {
        /// <summary>
        /// The equipped character
        /// </summary>
        private ICharacter equipedCharacter;

        // The lists of entities
        private List<ICharacter> characters;
        private List<IArtifact> artifacts;
        private List<IWeapon> weapons;

        /// <summary>
        /// The section being displayed in the inventory
        /// </summary>
        private InventoryDisplay displayingSection;
    }
}
