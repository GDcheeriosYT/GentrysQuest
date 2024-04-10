using System.Collections.Generic;

namespace GentrysQuest.Game.Entity
{
    public class Inventory
    {
        private List<Character> characters = new();
        private List<Artifact> artifacts = new();
        private List<Weapon.Weapon> weapons = new();
    }
}
