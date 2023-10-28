using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;
using UnityEditor.Rendering.Universal;

public class UI_Shop : MonoBehaviour
{
    [SerializeField]
    private Transform container;
    [SerializeField]
    private Transform shopUpgradeTemplate;

    private void Awake()
    {
        //container = transform.Find("container");
        //shopUpgradeTemplate = transform.Find("shopUpgradeTemplate");
        shopUpgradeTemplate.gameObject.SetActive(true);
    }

    private void Start()
    {
        // putero violento
        CreateUpgradeButton(Upgrade.UpgradeType.RangeUpgraded, Upgrade.GetSprite(Upgrade.UpgradeType.RangeUpgraded), "Range Increase", "Increase your RANGE by an esplentastic 20%!",
            Upgrade.GetCost(Upgrade.UpgradeType.RangeUpgraded), 0);

        CreateUpgradeButton(Upgrade.UpgradeType.SpeedUpgraded, Upgrade.GetSprite(Upgrade.UpgradeType.SpeedUpgraded), "Speed Increase", "Increase your movement SPEED by an fantastic 50%!",
            Upgrade.GetCost(Upgrade.UpgradeType.SpeedUpgraded), 1);

        CreateUpgradeButton(Upgrade.UpgradeType.LifeStealUpgraded, Upgrade.GetSprite(Upgrade.UpgradeType.LifeStealUpgraded), "Life Steal", "Increase your LIFE STEAL by an incredible 100%!",
            Upgrade.GetCost(Upgrade.UpgradeType.LifeStealUpgraded), 2);

        CreateUpgradeButton(Upgrade.UpgradeType.ScaryUpgraded, Upgrade.GetSprite(Upgrade.UpgradeType.ScaryUpgraded), "Spooky Increase", "Increase your SPOOKY by an awesome 25%!",
            Upgrade.GetCost(Upgrade.UpgradeType.ScaryUpgraded), 3);

        CreateUpgradeButton(Upgrade.UpgradeType.ProyectileRangeUpgraded, Upgrade.GetSprite(Upgrade.UpgradeType.ProyectileRangeUpgraded), "Proyectile Range Increase", "Increase your PROYECTILE RANGE by an wooping 50%!",
            Upgrade.GetCost(Upgrade.UpgradeType.ProyectileRangeUpgraded), 4);
    }

    private void CreateUpgradeButton(Upgrade.UpgradeType upgradeType, Sprite upgradeSprite, string upgradeName, string upgradeDescription, int upgradeCost, int positionIndex)
    {
        Transform shopUpgradeTransform = Instantiate(shopUpgradeTemplate, container);
        RectTransform shopUpgradeRectTransform = shopUpgradeTransform.GetComponent<RectTransform>();

        float shopUpgradeWidth = 190f;
        shopUpgradeRectTransform.anchoredPosition = new Vector2(-shopUpgradeWidth * positionIndex, 0);

        shopUpgradeTransform.Find("upgradeName").GetComponent<TextMeshProUGUI>().SetText(upgradeName);
        shopUpgradeTransform.Find("upgradeCost").GetComponent<TextMeshProUGUI>().SetText(upgradeCost.ToString());
        shopUpgradeTransform.Find("upgradeDescription").GetComponent<TextMeshProUGUI>().SetText(upgradeDescription);

        shopUpgradeTransform.Find("upgradeIcon").GetComponent<Image>().sprite = upgradeSprite;

        shopUpgradeTransform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            TryBuyUpgrade(upgradeType);
        };
    }

    private void TryBuyUpgrade(Upgrade.UpgradeType upgradeType)
    {

    }
}
