using GentrysQuest.Game.Database;

namespace GentrysQuest.Game.Entity;

public class Character : Entity, ICharacter
{
    public ArtifactManager Artifacts { get; }

    public Character()
    {
        Artifacts = new ArtifactManager(this);
        Artifacts.OnChangeArtifact += UpdateStats;
    }

    public override void Damage(int amount)
    {
        base.Damage(amount);
        GameData.CurrentStats.AddToStat(StatTypes.DamageTaken, amount);
        GameData.CurrentStats.AddToStat(StatTypes.MostDamageTaken, amount);
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);
        GameData.CurrentStats.AddToStat(StatTypes.HealthGained, amount);
        GameData.CurrentStats.AddToStat(StatTypes.HealthGainedOnce, amount);
    }

    public override void Die()
    {
        base.Die();
        GameData.CurrentStats.AddToStat(StatTypes.Deaths);
        GameData.CurrentStats.Log();
    }

    public override void UpdateStats()
    {
        Stats.ResetAdditionalValues();
        int level = Experience.Level.Current.Value;
        int starRating = StarRating.Value;
        Difficulty = (byte)(1 + level / 20);

        Stats.Health.SetDefaultValue(
            CalculatePointBenefit(Difficulty * 100, Stats.Health.point, 100) +
            CalculatePointBenefit(level * 50, Stats.Health.point, 25) +
            CalculatePointBenefit(starRating * 15, Stats.Health.point, 25)
        );

        Stats.Attack.SetDefaultValue(
            CalculatePointBenefit(Difficulty * 8, Stats.Attack.point, 5) +
            CalculatePointBenefit(level * 2, Stats.Attack.point, 4) +
            CalculatePointBenefit(starRating, Stats.Attack.point, 3)
        );

        Stats.Defense.SetDefaultValue(
            CalculatePointBenefit(Difficulty * 6, Stats.Defense.point, 4) +
            CalculatePointBenefit(level * 1, Stats.Defense.point, 2) +
            CalculatePointBenefit(starRating, Stats.Defense.point, 3)
        );

        Stats.CritRate.SetDefaultValue(
            CalculatePointBenefit(0, Stats.CritRate.point, 5)
        );

        Stats.CritDamage.SetDefaultValue(
            CalculatePointBenefit(Difficulty * 5, Stats.CritDamage.point, 5) +
            CalculatePointBenefit(starRating, Stats.CritDamage.point, 2)
        );

        Stats.Speed.SetDefaultValue(
            0.5 +
            CalculatePointBenefit(0, Stats.Speed.point, 0.2)
        );

        Stats.AttackSpeed.SetDefaultValue(
            CalculatePointBenefit(0, Stats.AttackSpeed.point, 0.3)
        );

        if (Weapon != null) addToStat(Weapon.Buff);

        foreach (Artifact artifact in Artifacts.Get())
        {
            if (artifact != null)
            {
                addToStat(artifact.MainAttribute);

                foreach (Buff attribute in artifact.Attributes)
                {
                    addToStat(attribute);
                }
            }
        }

        base.UpdateStats();
    }

    private void addToStat(Buff attribute)
    {
        Stat stat = Stats.GetStat(attribute.StatType.ToString());
        stat.Add(attribute.IsPercent ? stat.GetPercentFromDefault((float)attribute.Value.Value) : attribute.Value.Value);
    }
}
