using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public enum UpgradeType
    {
        ScaryUpgraded,
        SpeedUpgraded,
        RangeUpgraded,
        LifeStealUpgraded,
        ProyectileRangeUpgraded
    }

    public static int GetCost(UpgradeType upgradeType)
    {
        switch(upgradeType)
        {
            default:
            case UpgradeType.ScaryUpgraded: return 20;
            case UpgradeType.SpeedUpgraded: return 6;
            case UpgradeType.RangeUpgraded: return 15;
            case UpgradeType.LifeStealUpgraded: return 1;
            case UpgradeType.ProyectileRangeUpgraded: return 25;
        }
    }

    public static Sprite GetSprite(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            default:
            case UpgradeType.ScaryUpgraded: return GameAssets.i.spriteScary;
            case UpgradeType.SpeedUpgraded: return GameAssets.i.spriteSpeed;
            case UpgradeType.RangeUpgraded: return GameAssets.i.spriteRange;
            case UpgradeType.LifeStealUpgraded: return GameAssets.i.spriteLifeSteal;
            case UpgradeType.ProyectileRangeUpgraded: return GameAssets.i.spriteShootRange;
        }
    }
}
