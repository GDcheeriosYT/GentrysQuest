using GentrysQuest.Game.Content.Characters;
using GentrysQuest.Game.Content.Weapons;
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

            GameData.Characters.Add(new TestCharacter(1));
            GameData.Characters.Add(new TestCharacter(2));
            GameData.Characters.Add(new TestCharacter(3));
            GameData.Characters.Add(new TestCharacter(4));
            GameData.Characters.Add(new TestCharacter(5));
            GameData.Characters.Add(new BraydenMesserschmidt());

            GameData.Weapons.Add(new Knife());
            GameData.Weapons.Add(new BraydensOsuPen());

            Add(inventoryOverlay = new InventoryOverlay());
        }
    }
}
