using GentrysQuest.Game.Entity.Drawables;
using GentrysQuest.Game.Entity.Weapon;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;

namespace GentrysQuest.Game.Tests.Visual.Entity
{
    [TestFixture]
    public partial class TestSceneWeapon : GentrysQuestTestScene
    {
        private Weapon testWeapon;
        private DrawableWeapon testDrawableWeapon;

        public TestSceneWeapon()
        {
            Add(new Box { RelativeSizeAxes = Axes.Both, Colour = Colour4.Gray });
        }
    }
}
