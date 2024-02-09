using NUnit.Framework;

namespace GentrysQuest.Game.Tests.Visual.Entity;

[TestFixture]
public partial class TestSceneEntity : GentrysQuestTestScene
{
    private GentrysQuest.Game.Entity.Entity entity;

    public TestSceneEntity()
    {
        entity = new GentrysQuest.Game.Entity.Entity();
    }

    // [Test]
    // public void Start()
    // {
    //     AddStep()
    // }
}
