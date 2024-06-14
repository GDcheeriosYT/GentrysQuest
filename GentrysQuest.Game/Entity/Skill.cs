using GentrysQuest.Game.Graphics;
using GentrysQuest.Game.Utils;

namespace GentrysQuest.Game.Entity
{
    public abstract class Skill(Entity skillHaver)
    {
        /// <summary>
        /// The name of the skill
        /// </summary>
        public abstract string Name { get; protected set; }

        /// <summary>
        /// The description of the skill
        /// </summary>
        public abstract string Description { get; protected set; }

        /// <summary>
        /// The cooldown for the skill
        /// </summary>
        public abstract double Cooldown { get; protected set; }

        /// <summary>
        /// The time since last start
        /// </summary>
        public double TimeActed = 0;

        /// <summary>
        /// How much longer until the skill is ready to use again
        /// </summary>
        /// <returns></returns>
        public int PercentToDone = 0;

        /// <summary>
        /// Who has this skill?
        /// I know haver isn't a word...
        /// </summary>
        public Entity SkillHaver { get; } = skillHaver;

        /// <summary>
        /// How this skill works
        /// </summary>
        public abstract void Act();

        /// <summary>
        /// The texture mapping for this skill
        /// </summary>
        public TextureMapping TextureMapping { get; protected set; } = new();

        public void SetPercent(double currentTime) => PercentToDone = (int)(new ElapsedTime(currentTime, TimeActed) / Cooldown);
    }
}
