using GentrysQuest.Game.Entity;
using GentrysQuest.Game.Entity.Drawables;
using NUnit.Framework;
using osu.Framework.Logging;

namespace GentrysQuest.Game.Tests.Visual.Entity
{
    [TestFixture]
    public partial class TestSceneArtifactStats : GentrysQuestTestScene
    {
        private int level = 1;
        private int rating = 1;
        private bool isPercent = false;
        private StatDrawableContainer statContainer = new StatDrawableContainer();

        public TestSceneArtifactStats()
        {
            Add(statContainer);
            AddStep("ok!", () =>
            {
                Logger.Log("I'm ready!");
            });
            AddStep("Reset", resetDisplay);
            AddToggleStep("Is percent", b => toggleBool());
            AddSliderStep("Star Rating", 1, 5, 1, setStarRating);
            AddSliderStep("Level", 1, 20, 1, setLevel);

            statContainer.AddStat(new StatDrawable("Health", (float)getBuff(StatType.Health).Value.Value, true));
            statContainer.AddStat(new StatDrawable("Attack", (float)getBuff(StatType.Attack).Value.Value, true));
            statContainer.AddStat(new StatDrawable("Defense", (float)getBuff(StatType.Defense).Value.Value, true));
            statContainer.AddStat(new StatDrawable("CritRate", (float)getBuff(StatType.CritRate).Value.Value, true));
            statContainer.AddStat(new StatDrawable("CritDamage", (float)getBuff(StatType.CritDamage).Value.Value, true));
            statContainer.AddStat(new StatDrawable("Speed", (float)getBuff(StatType.Speed).Value.Value, true));
            statContainer.AddStat(new StatDrawable("AttackSpeed", (float)getBuff(StatType.AttackSpeed).Value.Value, true));
            statContainer.AddStat(new StatDrawable("RegenSpeed", (float)getBuff(StatType.RegenSpeed).Value.Value, true));
            statContainer.AddStat(new StatDrawable("RegenStrength", (float)getBuff(StatType.RegenStrength).Value.Value, true));
        }

        private void resetDisplay()
        {
            statContainer.GetStatDrawable("Health").UpdateValue((float)getBuff(StatType.Health).Value.Value);
            statContainer.GetStatDrawable("Attack").UpdateValue((float)getBuff(StatType.Attack).Value.Value);
            statContainer.GetStatDrawable("Defense").UpdateValue((float)getBuff(StatType.Defense).Value.Value);
            statContainer.GetStatDrawable("CritRate").UpdateValue((float)getBuff(StatType.CritRate).Value.Value);
            statContainer.GetStatDrawable("CritDamage").UpdateValue((float)getBuff(StatType.CritDamage).Value.Value);
            statContainer.GetStatDrawable("Speed").UpdateValue((float)getBuff(StatType.Speed).Value.Value);
            statContainer.GetStatDrawable("AttackSpeed").UpdateValue((float)getBuff(StatType.AttackSpeed).Value.Value);
            statContainer.GetStatDrawable("RegenSpeed").UpdateValue((float)getBuff(StatType.RegenSpeed).Value.Value);
            statContainer.GetStatDrawable("RegenStrength").UpdateValue((float)getBuff(StatType.RegenStrength).Value.Value);
        }

        private Buff getBuff(StatType buffType)
        {
            Artifact artifact = new Artifact();
            artifact.StarRating.Value = rating;
            artifact.Experience.Level.Current.Value = level;
            Buff buff = new Buff(artifact, buffType, isPercent);

            for (int i = 0; i < level; i++)
            {
                buff.Improve();
            }

            return buff;
        }

        private void toggleBool()
        {
            isPercent = !isPercent;
            resetDisplay();
        }

        private void setLevel(int level)
        {
            this.level = level;
            resetDisplay();
        }

        private void setStarRating(int rating)
        {
            this.rating = rating;
            resetDisplay();
        }
    }
}
