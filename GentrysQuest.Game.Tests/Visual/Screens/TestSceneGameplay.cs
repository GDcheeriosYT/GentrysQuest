using GentrysQuest.Game.Content.Characters;
using GentrysQuest.Game.Content.Weapons;
using GentrysQuest.Game.Database;
using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Weapon;
using GentrysQuest.Game.Screens.Gameplay;
using NUnit.Framework;
using osu.Framework.Screens;

namespace GentrysQuest.Game.Tests.Visual.Screens
{
    [TestFixture]
    public partial class TestSceneGameplay : GentrysQuestTestScene
    {
        private ScreenStack screens;
        private Gameplay gameplay;
        private Character theGuy;
        private Weapon testWeapon;

        public TestSceneGameplay()
        {
            theGuy = new BraydenMesserschmidt();
            testWeapon = new BraydensOsuPen();
            GameData.EquipCharacter(theGuy);
            GameData.Characters.Add(theGuy);
            GameData.Characters.Add(new TestCharacter(1));
            theGuy.SetWeapon(testWeapon);
            Add(screens = new ScreenStack());
            screens.Push(gameplay = new Gameplay());
        }

        [Test]
        public void Management()
        {
            AddStep("AddEnemy", () =>
            {
                gameplay.AddEnemy(theGuy.Experience.Level.Current.Value);
            });
            AddStep("Damage", (() => theGuy.Damage(10)));
            AddStep("Spawn Enemys", () => gameplay.SpawnEntities());
        }
    }
}
