using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        CreateUpgradeButton(Upgrade.GetSprite(Upgrade.UpgradeType.RangeUpgraded), "Range Increase", "Increase your RANGE by an incredible 20%!",
            Upgrade.GetCost(Upgrade.UpgradeType.RangeUpgraded), 0);

        CreateUpgradeButton(Upgrade.GetSprite(Upgrade.UpgradeType.SpeedUpgraded), "Speed Increase", "Increase your movement SPEED by an incredible 50%!",
            Upgrade.GetCost(Upgrade.UpgradeType.SpeedUpgraded), 1);

        CreateUpgradeButton(Upgrade.GetSprite(Upgrade.UpgradeType.LifeStealUpgraded), "Life Steal", "Increase your LIFE STEAL by an incredible 100%!",
            Upgrade.GetCost(Upgrade.UpgradeType.LifeStealUpgraded), 2);
    }

    private void CreateUpgradeButton(Sprite upgradeSprite, string upgradeName, string upgradeDescription, int upgradeCost, int positionIndex)
    {
        Transform shopUpgradeTransform = Instantiate(shopUpgradeTemplate, container);
        RectTransform shopUpgradeRectTransform = shopUpgradeTransform.GetComponent<RectTransform>();

        float shopUpgradeWidth = 300f;
        shopUpgradeRectTransform.anchoredPosition = new Vector2(-shopUpgradeWidth * positionIndex, 0);

        shopUpgradeTransform.Find("upgradeName").GetComponent<TextMeshProUGUI>().SetText(upgradeName);
        shopUpgradeTransform.Find("upgradeCost").GetComponent<TextMeshProUGUI>().SetText(upgradeCost.ToString());
        shopUpgradeTransform.Find("upgradeDescription").GetComponent<TextMeshProUGUI>().SetText(upgradeDescription);

        shopUpgradeTransform.Find("upgradeIcon").GetComponent<Image>().sprite = upgradeSprite;
    }
}
