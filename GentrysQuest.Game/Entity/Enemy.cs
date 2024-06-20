using System;

namespace GentrysQuest.Game.Entity;

public class Enemy : Entity
{
    public WeaponChoices WeaponChoices = new();

    public Enemy()
        : base()
    {
    }

    public override void UpdateStats()
    {
        int level = Experience.Level.Current.Value;

        Stats.Health.SetDefaultValue(
            Math.Pow(Difficulty, 3) * (2000 * (Stats.Health.point + 1)) +
            level * 25 * (Stats.Health.point + 1)
        );

        Stats.Attack.SetDefaultValue(
            2 +
            CalculatePointBenefit(Math.Pow(Difficulty, 2.5) * 15, Stats.Attack.point, 20) +
            CalculatePointBenefit(level * 1, Stats.Attack.point, 5)
        );

        Stats.Defense.SetDefaultValue(
            CalculatePointBenefit(Difficulty * 100, Stats.Defense.point, 18) +
            CalculatePointBenefit(level * 4, Stats.Defense.point, 2)
        );

        Stats.CritRate.SetDefaultValue(20);

        Stats.CritDamage.SetDefaultValue(Difficulty * 20);

        Stats.Speed.SetDefaultValue(
            CalculatePointBenefit(0, Stats.Speed.point, 0.2)
        );

        Stats.AttackSpeed.SetDefaultValue(
            CalculatePointBenefit(0, Stats.AttackSpeed.point, 0.3)
        );

        base.UpdateStats();
    }

    public void SetWeapon() => SetWeapon(WeaponChoices.GetChoice());
}
