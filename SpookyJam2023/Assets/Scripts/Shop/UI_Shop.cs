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

    [SerializeField]
    public TextMeshProUGUI moneyText;

    //public TextMeshProUGUI upgradeCostText;
    private Dictionary<Transform, Upgrade.UpgradeType> upgradeTypeDict = new Dictionary<Transform, Upgrade.UpgradeType>();

    private const int maxUpgrade = 5;

    private void Awake()
    {
        //container = transform.Find("container");
        //shopUpgradeTemplate = transform.Find("shopUpgradeTemplate");
        shopUpgradeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        UpdateMoneyText();
        // putero violento
        CreateUpgradeButton(Upgrade.UpgradeType.DamageUpgraded, Upgrade.GetSprite(Upgrade.UpgradeType.DamageUpgraded), "Damage Increase", "Increase your DAMAGE by an esplentastic 10%!",
            Upgrade.GetCost(Upgrade.UpgradeType.DamageUpgraded), 0);

        CreateUpgradeButton(Upgrade.UpgradeType.SpeedUpgraded, Upgrade.GetSprite(Upgrade.UpgradeType.SpeedUpgraded), "Speed Increase", "Increase your movement SPEED by a fantastic 10%!",
            Upgrade.GetCost(Upgrade.UpgradeType.SpeedUpgraded), 1);

        CreateUpgradeButton(Upgrade.UpgradeType.LifeStealUpgraded, Upgrade.GetSprite(Upgrade.UpgradeType.LifeStealUpgraded), "Life Steal", "Increase your LIFE STEAL by an incredible 10%!",
            Upgrade.GetCost(Upgrade.UpgradeType.LifeStealUpgraded), 2);

        CreateUpgradeButton(Upgrade.UpgradeType.ScaryUpgraded, Upgrade.GetSprite(Upgrade.UpgradeType.ScaryUpgraded), "Spooky Increase", "Increase your SPOOKYNESS by an awesome 10%!",
            Upgrade.GetCost(Upgrade.UpgradeType.ScaryUpgraded), 3);

        CreateUpgradeButton(Upgrade.UpgradeType.ProyectileRangeUpgraded, Upgrade.GetSprite(Upgrade.UpgradeType.ProyectileRangeUpgraded), "Range Increase", "Increase your RANGE by a wooping 10%!",
            Upgrade.GetCost(Upgrade.UpgradeType.ProyectileRangeUpgraded), 4);
    }

    private void CreateUpgradeButton(Upgrade.UpgradeType upgradeType, Sprite upgradeSprite, string upgradeName, string upgradeDescription, int upgradeCost, int positionIndex)
    {
        Transform shopUpgradeTransform = Instantiate(shopUpgradeTemplate, container);
        shopUpgradeTransform.gameObject.SetActive(true);
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
                    shopUpgradeTransform.GetComponent<Button_UI>().ClickFunc = () =>
                    {
                        TryBuyUpgrade(upgradeType);
                    };
                break;
            case Upgrade.UpgradeType.ProyectileRangeUpgraded:
                    shopUpgradeTransform.GetComponent<Button_UI>().ClickFunc = () =>
                    {
                        TryBuyUpgrade(upgradeType);
                    };
                break;
            case Upgrade.UpgradeType.LifeStealUpgraded:
                    shopUpgradeTransform.GetComponent<Button_UI>().ClickFunc = () =>
                    {
                        TryBuyUpgrade(upgradeType);
                    };

                break;
            case Upgrade.UpgradeType.SpeedUpgraded:
                    shopUpgradeTransform.GetComponent<Button_UI>().ClickFunc = () =>
                    {
                        TryBuyUpgrade(upgradeType);
                    };
                break;
            case Upgrade.UpgradeType.ScaryUpgraded:

                    shopUpgradeTransform.GetComponent<Button_UI>().ClickFunc = () =>
                    {
                        TryBuyUpgrade(upgradeType);
                    };
                break;
            default:
                Debug.LogError("ni puta idea de mejora: " + upgradeType);
                break;
        }
        upgradeTypeDict[shopUpgradeTransform] = upgradeType;
    }

    private void TryBuyUpgrade(Upgrade.UpgradeType upgradeType)
    {

        switch (upgradeType)
        {
            case Upgrade.UpgradeType.DamageUpgraded:
                if(UpgradeStats.moneyAmount >= Upgrade.GetCost(upgradeType))
                {
                    UpgradeStats.moneyAmount = UpgradeStats.moneyAmount - Upgrade.GetCost(upgradeType);
                    UpgradeStats.IndexDamage = Mathf.Min(UpgradeStats.IndexDamage + 1, maxUpgrade);
                    UpdateCost();
                    UpdateMoneyText();

                    Debug.Log("money avaliable: " + UpgradeStats.moneyAmount);
                    Debug.Log("upgrade cost: " + UpgradeStats.IndexDamage);
                }
                //Debug.Log(UpgradeStats.IndexDamage);
                break;
            case Upgrade.UpgradeType.ProyectileRangeUpgraded:
                if (UpgradeStats.moneyAmount >= Upgrade.GetCost(upgradeType))
                {
                    UpgradeStats.moneyAmount = UpgradeStats.moneyAmount - Upgrade.GetCost(upgradeType);
                    UpgradeStats.IndexRange = Mathf.Min(UpgradeStats.IndexRange + 1, maxUpgrade);
                    UpdateCost();
                    UpdateMoneyText();
                }
                break;
            case Upgrade.UpgradeType.LifeStealUpgraded:
                if (UpgradeStats.moneyAmount >= Upgrade.GetCost(upgradeType))
                {
                    UpgradeStats.moneyAmount = UpgradeStats.moneyAmount - Upgrade.GetCost(upgradeType);
                    UpgradeStats.IndexLifeSteal = Mathf.Min(UpgradeStats.IndexLifeSteal + 1, maxUpgrade);
                    UpdateCost();
                    UpdateMoneyText();
                }
                break;
            case Upgrade.UpgradeType.SpeedUpgraded:
                if (UpgradeStats.moneyAmount >= Upgrade.GetCost(upgradeType))
                {
                    UpgradeStats.moneyAmount = UpgradeStats.moneyAmount - Upgrade.GetCost(upgradeType);
                    UpgradeStats.IndexSpeed = Mathf.Min(UpgradeStats.IndexSpeed + 1, maxUpgrade);
                    UpdateCost();
                    UpdateMoneyText();
                }
                break;
            case Upgrade.UpgradeType.ScaryUpgraded:
                if (UpgradeStats.moneyAmount >= Upgrade.GetCost(upgradeType))
                {
                    UpgradeStats.moneyAmount = UpgradeStats.moneyAmount - Upgrade.GetCost(upgradeType);
                    UpgradeStats.IndexScary = Mathf.Min(UpgradeStats.IndexScary + 1, maxUpgrade);
                    UpdateCost();
                    UpdateMoneyText();
                }
                break;
            default:
                Debug.LogError("ni puta idea de mejora: " + upgradeType);
                break;
        }
    }

    private void UpdateMoneyText()
    {
        moneyText.SetText("");
        moneyText.SetText(UpgradeStats.moneyAmount.ToString());
    }

    private void UpdateCost()
    {
        foreach (Transform child in container)
        {
            Upgrade.UpgradeType upgradeType;
            if (upgradeTypeDict.TryGetValue(child, out upgradeType))
            {
                TextMeshProUGUI costText = child.Find("upgradeCost").GetComponent<TextMeshProUGUI>();
                costText.SetText(Upgrade.GetCost(upgradeType).ToString());
            }
        }
    }
}
