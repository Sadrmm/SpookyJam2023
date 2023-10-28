using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public enum UpgradeType
    {
        ScaryUpgraded,
        SpeedUpgraded,
        DamageUpgraded,
        LifeStealUpgraded,
        ProyectileRangeUpgraded
    }

    public static int GetCost(UpgradeType upgradeType)
    {
        switch(upgradeType)
        {
            default:
            case UpgradeType.ScaryUpgraded: return 20 * (UpgradeStats.IndexScary + 1);
            case UpgradeType.SpeedUpgraded: return 6 * (UpgradeStats.IndexSpeed + 1);
            case UpgradeType.DamageUpgraded: return 15 * (UpgradeStats.IndexDamage + 1);
            case UpgradeType.LifeStealUpgraded: return 1 * (UpgradeStats.IndexLifeSteal + 1);
            case UpgradeType.ProyectileRangeUpgraded: return 25 * (UpgradeStats.IndexRange + 1);
        }
    }

    public static Sprite GetSprite(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            default:
            case UpgradeType.ScaryUpgraded: return GameAssets.i.spriteScary;
            case UpgradeType.SpeedUpgraded: return GameAssets.i.spriteSpeed;
            case UpgradeType.DamageUpgraded: return GameAssets.i.spriteDamage;
            case UpgradeType.LifeStealUpgraded: return GameAssets.i.spriteLifeSteal;
            case UpgradeType.ProyectileRangeUpgraded: return GameAssets.i.spriteShootRange;
        }
    }
}
