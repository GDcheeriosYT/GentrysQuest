namespace GentrysQuest.Game.Entity;

public class Character : Entity, ICharacter
{
    public ArtifactManager Artifacts { get; } = new ArtifactManager();

    public override void UpdateStats()
    {
        int level = Experience.Level.Current.Value;
        int starRating = StarRating.Value;
        difficulty = 1 + level / 20;

        Stats.Health.SetDefaultValue(
            CalculatePointBenefit(difficulty * 100, Stats.Health.point, 500) +
            CalculatePointBenefit(level * 50, Stats.Health.point, 10) +
            CalculatePointBenefit(starRating * 50, Stats.Health.point, 50)
        );

        Stats.Attack.SetDefaultValue(
            CalculatePointBenefit(difficulty * 8, Stats.Attack.point, 5) +
            CalculatePointBenefit(level * 2, Stats.Attack.point, 4) +
            CalculatePointBenefit(starRating * 5, Stats.Attack.point, 3)
        );

        Stats.Defense.SetDefaultValue(
            CalculatePointBenefit(difficulty * 10, Stats.Defense.point, 4) +
            CalculatePointBenefit(level * 2, Stats.Defense.point, 2) +
            CalculatePointBenefit(starRating * 3, Stats.Defense.point, 3)
        );

        Stats.CritRate.SetDefaultValue(
            CalculatePointBenefit(5, Stats.CritRate.point, 5) +
            difficulty + 1
        );

        Stats.CritDamage.SetDefaultValue(
            CalculatePointBenefit(difficulty * 5, Stats.CritDamage.point, 1) +
            CalculatePointBenefit(starRating * 1, Stats.CritDamage.point, 1)
        );

        Stats.Speed.SetDefaultValue(
            0.5 +
            CalculatePointBenefit(0, Stats.Speed.point, 0.2)
        );

        Stats.AttackSpeed.SetDefaultValue(
            CalculatePointBenefit(0, Stats.AttackSpeed.point, 0.3)
        );

        base.UpdateStats();
    }
}
