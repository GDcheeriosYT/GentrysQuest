using GentrysQuest.Game.Entity.Drawables;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace GentrysQuest.Game.Tests.Visual.Entity
{
    [TestFixture]
    public partial class TestSceneStatDrawable : GentrysQuestTestScene
    {
        private float stat1 = 10;
        private float stat2 = 5;
        private StatDrawable statDrawable1;
        private StatDrawable statDrawable2;

        public TestSceneStatDrawable()
        {
            Add(new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Colour4.Gray
            });
            Add(statDrawable1 = new StatDrawable("Poop Stat", stat1, true)
            {
                Position = new Vector2(0, 200)
            });
            Add(statDrawable2 = new StatDrawable("Minor Poop Stat", stat2, false)
            {
                Position = new Vector2(0, 400)
            });
        }

        [Test]
        public void StatTesting()
        {
            AddStep("IncrementTheThings", () =>
            {
                stat1 += 1;
                stat2 += 0.5f;
                statDrawable1.UpdateValue(stat1);
                statDrawable2.UpdateValue(stat2);
            });
        }
    }
}
