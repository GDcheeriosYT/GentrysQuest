using System.Collections.Generic;
using GentrysQuest.Game.Utils;
using osu.Framework.Logging;

namespace GentrysQuest.Game.Entity;

public class Enemy : Entity
{
    protected List<Weapon.Weapon> WeaponChoices = new();

    public Enemy()
        : base()
    {
    }

    public override void UpdateStats()
    {
        int level = Experience.Level.Current.Value;
        int starRating = StarRating.Value;

        Stats.Health.SetDefaultValue(
            CalculatePointBenefit(Difficulty * 2000, Stats.Health.point, 500) +
            CalculatePointBenefit(level * 100, Stats.Health.point, 15)
        );

        Stats.Attack.SetDefaultValue(
            CalculatePointBenefit(Difficulty * 50, Stats.Attack.point, 20) +
            CalculatePointBenefit(level * 5, Stats.Attack.point, 5)
        );

        Stats.Defense.SetDefaultValue(
            CalculatePointBenefit(Difficulty * 20, Stats.Defense.point, 18) +
            CalculatePointBenefit(level * 4, Stats.Defense.point, 2)
        );

        Stats.CritRate.SetDefaultValue(5);

        Stats.CritDamage.SetDefaultValue(Difficulty * 20);

        Stats.Speed.SetDefaultValue(
            CalculatePointBenefit(0, Stats.Speed.point, 0.2)
        );

        Stats.AttackSpeed.SetDefaultValue(
            CalculatePointBenefit(0, Stats.AttackSpeed.point, 0.3)
        );

        base.UpdateStats();
    }

    public void SetWeapon()
    {
        Logger.Log(WeaponChoices.Count.ToString());
        SetWeapon(WeaponChoices[MathBase.RandomInt(0, WeaponChoices.Count)]);
        Logger.Log(Weapon?.ToString());
    }
}
