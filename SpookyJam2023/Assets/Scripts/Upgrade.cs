using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public enum UpgradeType
    {
        ScaryNone,
        ScaryUpgraded,
        SpeedNone,
        SpeedUpgraded,
        RangeNone,
        RangeUpgraded,
        LifeStealNone,
        LifeStealUpgraded
    }

    public static int GetCost(UpgradeType upgradeType)
    {
        switch(upgradeType)
        {
            default:
            case UpgradeType.ScaryNone: return 10;
            case UpgradeType.ScaryUpgraded: return 20;
            case UpgradeType.SpeedNone: return 5;
            case UpgradeType.SpeedUpgraded: return 6;
            case UpgradeType.RangeNone: return 10;
            case UpgradeType.RangeUpgraded: return 15;
            case UpgradeType.LifeStealNone: return 0;
            case UpgradeType.LifeStealUpgraded: return 1;
        }
    }

    public static Sprite GetSprite(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            default:
            case UpgradeType.ScaryNone: return GameAssets.i.spriteScary;
            case UpgradeType.ScaryUpgraded: return GameAssets.i.spriteScary;
            case UpgradeType.SpeedNone: return GameAssets.i.spriteSpeed;
            case UpgradeType.SpeedUpgraded: return GameAssets.i.spriteSpeed;
            case UpgradeType.RangeNone: return GameAssets.i.spriteRange;
            case UpgradeType.RangeUpgraded: return GameAssets.i.spriteRange;
            case UpgradeType.LifeStealNone: return GameAssets.i.spriteLifeSteal;
            case UpgradeType.LifeStealUpgraded: return GameAssets.i.spriteLifeSteal;
        }
    }
}
