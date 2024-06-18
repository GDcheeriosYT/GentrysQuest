using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Utils;

namespace GentrysQuest.Game.Content.Skills
{
    public class Teleport(Entity.Entity skillHaver) : Skill(skillHaver)
    {
        public override string Name { get; protected set; } = "Teleport";
        public override string Description { get; protected set; } = "Teleport in the direction of movement";
        public override double Cooldown { get; protected set; } = new Second(7);
        public override int MaxStack { get; protected set; } = 3;

        public override void Act()
        {
            base.Act();

            SkillHaver.PositionJump = 20;
        }
    }
}
