using GentrysQuest.Game.Database;
using osu.Framework.Testing;

namespace GentrysQuest.Game.Tests.Visual
{
    public partial class GentrysQuestTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new GentrysQuestTestSceneTestRunner();

        private partial class GentrysQuestTestSceneTestRunner : GentrysQuestGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }

        public GentrysQuestTestScene()
        {
            GameData.Reset();
        }
    }
}
