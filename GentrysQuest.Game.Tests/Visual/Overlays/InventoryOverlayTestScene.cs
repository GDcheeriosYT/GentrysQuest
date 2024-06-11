using GentrysQuest.Game.Content.Characters;
using GentrysQuest.Game.Content.Families;
using GentrysQuest.Game.Database;
using GentrysQuest.Game.Overlays.Inventory;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Shapes;

namespace GentrysQuest.Game.Tests.Visual.Overlays
{
    [TestFixture]
    public partial class InventoryOverlayTestScene : GentrysQuestTestScene
    {
        private InventoryOverlay inventoryOverlay;

        public InventoryOverlayTestScene()
        {
            Add(new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = ColourInfo.GradientVertical(Colour4.Black, Colour4.White)
            });

            GameData.Money.InfiniteMoney = true;

            GameData.Add(new TestCharacter(1));
            GameData.Add(new TestCharacter(2));
            GameData.Add(new TestCharacter(3));
            GameData.Add(new TestCharacter(4));
            GameData.Add(new TestCharacter(5));

            foreach (var character in GameData.Content.Characters)
            {
                GameData.Add(character);
            }

            foreach (var weapon in GameData.Content.Weapons)
            {
                GameData.Add(weapon);
            }

            Add(inventoryOverlay = new InventoryOverlay());
        }

        [Test]
        public void Display()
        {
            AddStep("Show", () => inventoryOverlay.Show());
            AddStep("Hide", () => inventoryOverlay.Hide());
        }

        [Test]
        public void Collection()
        {
            AddStep("Add Artifact", () => GameData.Add(new TestArtifact()));
        }
    }
}
