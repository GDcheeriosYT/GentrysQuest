using GentrysQuest.Game.Content.Characters;
using GentrysQuest.Game.Content.Weapons;
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
            Add(screens = new ScreenStack());
            screens.Push(gameplay = new Gameplay());
        }

        [Test]
        public virtual void Start()
        {
            AddStep("start", () =>
            {
                theGuy = new BraydenMesserschmidt();
                testWeapon = new BraydensOsuPen();
                theGuy.SetWeapon(testWeapon);
                gameplay.SetUp(theGuy);
            });
            AddStep("End", () => gameplay.End());
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
