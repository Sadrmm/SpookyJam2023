using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;
using UnityEditor.Rendering.Universal;
using Unity.VisualScripting;

public class UI_Shop : MonoBehaviour
{
    [SerializeField]
    private Transform container;
    [SerializeField]
    private Transform shopUpgradeTemplate;

    private int maxUpgrade = 5;

    private void Awake()
    {
        //container = transform.Find("container");
        //shopUpgradeTemplate = transform.Find("shopUpgradeTemplate");
        shopUpgradeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        // putero violento
        CreateUpgradeButton(Upgrade.UpgradeType.DamageUpgraded, Upgrade.GetSprite(Upgrade.UpgradeType.DamageUpgraded), "Damage Increase", "Increase your DAAMGE by an esplentastic 10%!",
            Upgrade.GetCost(Upgrade.UpgradeType.DamageUpgraded), 0);

        CreateUpgradeButton(Upgrade.UpgradeType.SpeedUpgraded, Upgrade.GetSprite(Upgrade.UpgradeType.SpeedUpgraded), "Speed Increase", "Increase your movement SPEED by an fantastic 10%!",
            Upgrade.GetCost(Upgrade.UpgradeType.SpeedUpgraded), 1);

        CreateUpgradeButton(Upgrade.UpgradeType.LifeStealUpgraded, Upgrade.GetSprite(Upgrade.UpgradeType.LifeStealUpgraded), "Life Steal", "Increase your LIFE STEAL by an incredible 10%!",
            Upgrade.GetCost(Upgrade.UpgradeType.LifeStealUpgraded), 2);

        CreateUpgradeButton(Upgrade.UpgradeType.ScaryUpgraded, Upgrade.GetSprite(Upgrade.UpgradeType.ScaryUpgraded), "Spooky Increase", "Increase your SPOOKY by an awesome 10%!",
            Upgrade.GetCost(Upgrade.UpgradeType.ScaryUpgraded), 3);

        CreateUpgradeButton(Upgrade.UpgradeType.ProyectileRangeUpgraded, Upgrade.GetSprite(Upgrade.UpgradeType.ProyectileRangeUpgraded), "Proyectile Range Increase", "Increase your PROYECTILE RANGE by an wooping 10%!",
            Upgrade.GetCost(Upgrade.UpgradeType.ProyectileRangeUpgraded), 4);
    }

    private void CreateUpgradeButton(Upgrade.UpgradeType upgradeType, Sprite upgradeSprite, string upgradeName, string upgradeDescription, int upgradeCost, int positionIndex)
    {
        Transform shopUpgradeTransform = Instantiate(shopUpgradeTemplate, container);
        RectTransform shopUpgradeRectTransform = shopUpgradeTransform.GetComponent<RectTransform>();

        //float shopUpgradeWidth = 190f;
        //shopUpgradeRectTransform.anchoredPosition = new Vector2(-shopUpgradeWidth * positionIndex, 0);

        shopUpgradeTransform.Find("upgradeName").GetComponent<TextMeshProUGUI>().SetText(upgradeName);
        shopUpgradeTransform.Find("upgradeCost").GetComponent<TextMeshProUGUI>().SetText(upgradeCost.ToString());
        shopUpgradeTransform.Find("upgradeDescription").GetComponent<TextMeshProUGUI>().SetText(upgradeDescription);

        shopUpgradeTransform.Find("upgradeIcon").GetComponent<Image>().sprite = upgradeSprite;

        switch (upgradeType)
        {
            case Upgrade.UpgradeType.DamageUpgraded:
                if(UpgradeStats.IndexDamage >= maxUpgrade) 
                {
                    shopUpgradeTransform.Find("offFilter").gameObject.SetActive(true); 
                }
                else
                {
                    shopUpgradeTransform.GetComponent<Button_UI>().ClickFunc = () =>
                    {
                        TryBuyUpgrade(upgradeType);
                    };
                }
                break;
            case Upgrade.UpgradeType.ProyectileRangeUpgraded:
                if (UpgradeStats.IndexRange >= maxUpgrade)
                {
                    shopUpgradeTransform.Find("offFilter").gameObject.SetActive(true);
                }
                else
                {
                    shopUpgradeTransform.GetComponent<Button_UI>().ClickFunc = () =>
                    {
                        TryBuyUpgrade(upgradeType);
                    };
                }
                break;
            case Upgrade.UpgradeType.LifeStealUpgraded:
                if (UpgradeStats.IndexLifeSteal >= maxUpgrade)
                {
                    shopUpgradeTransform.Find("offFilter").gameObject.SetActive(true);
                }
                else
                {
                    shopUpgradeTransform.GetComponent<Button_UI>().ClickFunc = () =>
                    {
                        TryBuyUpgrade(upgradeType);
                    };
                }
                break;
            case Upgrade.UpgradeType.SpeedUpgraded:
                if (UpgradeStats.IndexSpeed >= maxUpgrade)
                {
                    shopUpgradeTransform.Find("offFilter").gameObject.SetActive(true);
                }
                else
                {
                    shopUpgradeTransform.GetComponent<Button_UI>().ClickFunc = () =>
                    {
                        TryBuyUpgrade(upgradeType);
                    };
                }
                break;
            case Upgrade.UpgradeType.ScaryUpgraded:
                if (UpgradeStats.IndexScary >= maxUpgrade)
                {
                    shopUpgradeTransform.Find("offFilter").gameObject.SetActive(true);
                }
                else
                {
                    shopUpgradeTransform.GetComponent<Button_UI>().ClickFunc = () =>
                    {
                        TryBuyUpgrade(upgradeType);
                    };
                }
                break;
            default:
                Debug.LogError("ni puta idea de mejora: " + upgradeType);
                break;
        }
    }

    private void TryBuyUpgrade(Upgrade.UpgradeType upgradeType)
    {
        Debug.Log("You bought the: " + upgradeType + "!!!");

        switch (upgradeType)
        {
            case Upgrade.UpgradeType.DamageUpgraded:
                UpgradeStats.IndexDamage = Mathf.Min(UpgradeStats.IndexDamage + 1, maxUpgrade);
                //Debug.Log(UpgradeStats.IndexDamage);
                break;
            case Upgrade.UpgradeType.ProyectileRangeUpgraded:
                UpgradeStats.IndexRange = Mathf.Min(UpgradeStats.IndexRange + 1, maxUpgrade);
                break;
            case Upgrade.UpgradeType.LifeStealUpgraded:
                UpgradeStats.IndexLifeSteal = Mathf.Min(UpgradeStats.IndexLifeSteal + 1, maxUpgrade);
                break;
            case Upgrade.UpgradeType.SpeedUpgraded:
                UpgradeStats.IndexSpeed = Mathf.Min(UpgradeStats.IndexSpeed + 1, maxUpgrade);
                break;
            case Upgrade.UpgradeType.ScaryUpgraded:
                UpgradeStats.IndexScary = Mathf.Min(UpgradeStats.IndexScary + 1, maxUpgrade);
                break;
            default:
                Debug.LogError("ni puta idea de mejora: " + upgradeType);
                break;
        }
    }
}
