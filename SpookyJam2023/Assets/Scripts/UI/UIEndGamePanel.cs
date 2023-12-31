using TMPro;
using UnityEngine;

public class UIEndGamePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _finalPercentajeText;
    [SerializeField] TextMeshProUGUI _finalGoldText;

    private void SetTexts(float finalPercentaje, int finalGold)
    {
        _finalPercentajeText.text = $"Your Percentaje is:\n{finalPercentaje.ToString("0.00")}";
        _finalGoldText.text = $"Your GOLD:\n{finalGold}";
    }
    public void Show()
    {
        gameObject.SetActive(true);
        SetTexts(PaintPercentageController.Instance.Percentage * 100, UpgradeStats.MoneyAmount);
    }
}
