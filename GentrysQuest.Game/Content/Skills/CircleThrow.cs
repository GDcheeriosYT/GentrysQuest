using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Utils;

namespace GentrysQuest.Game.Content.Skills
{
    public class CircleThrow(Entity.Entity skillHaver) : Skill(skillHaver)
    {
        public override string Name { get; protected set; } = "Circle Throw";
        public override string Description { get; protected set; } = "Brayden's amazing secondary skill";
        public override double Cooldown { get; protected set; } = new Second(5);

        public override void Act()
        {
            throw new System.NotImplementedException();
        }
    }
}
