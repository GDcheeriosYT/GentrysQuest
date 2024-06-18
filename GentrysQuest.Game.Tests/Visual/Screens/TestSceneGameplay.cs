using GentrysQuest.Game.Content.Characters;
using GentrysQuest.Game.Content.Effects;
using GentrysQuest.Game.Content.Families;
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
            theGuy.Artifacts.Equip(new TestArtifact(), 0);
            theGuy.Artifacts.Equip(new TestArtifact(), 1);
            theGuy.Artifacts.Equip(new TestArtifact(), 2);
            theGuy.Artifacts.Equip(new TestArtifact(), 3);
            theGuy.Artifacts.Equip(new TestArtifact(), 4);
            testWeapon = new BraydensOsuPen();
            GameData.EquipCharacter(theGuy);
            GameData.Money.InfiniteMoney = true;
            GameData.Add(theGuy);
            GameData.Add(new TestCharacter(1));
            theGuy.SetWeapon(testWeapon);
            Add(screens = new ScreenStack());
            screens.Push(gameplay = new Gameplay());
        }

        [Test]
        public void Gameplay()
        {
            AddStep("AddEnemy", () =>
            {
                gameplay.AddEnemy(theGuy.Experience.Level.Current.Value);
            });
            AddSliderStep("Difficulty", 0, 10, 0, i => gameplay.SetDifficulty(i));
            AddStep("Damage", (() => theGuy.Damage(10)));
            AddStep("Slow", () => theGuy.AddEffect(new Slowness()));
            AddStep("Burn", () => theGuy.AddEffect(new Burn()));
            AddStep("Spawn Enemys", () => gameplay.SpawnEntities());
            AddStep("End", () => gameplay.End());
        }
    }
}
