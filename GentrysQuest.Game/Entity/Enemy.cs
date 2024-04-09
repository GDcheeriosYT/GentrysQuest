using System.Collections.Generic;
using GentrysQuest.Game.Utils;
using osu.Framework.Logging;

namespace GentrysQuest.Game.Entity;

public class Enemy : Entity
{
    protected List<Weapon.Weapon> WeaponChoices = new();

    public Enemy() : base()
    {
    }

    public void SetWeapon()
    {
        Logger.Log(WeaponChoices.Count.ToString());
        SetWeapon(WeaponChoices.Count > 1 ? WeaponChoices[MathBase.RandomInt(0, WeaponChoices.Count - 1)] : WeaponChoices[0]);
        Logger.Log(Weapon?.ToString());
    }
}
