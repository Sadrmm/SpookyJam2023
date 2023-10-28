using TMPro;
using UnityEngine;

public class UIMapFilledPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _percentageText;

    private void OnEnable()
    {
        PaintPercentageController.Instance.OnPercentageCalculated += SetPercentageText;
    }

    private void OnDisable()
    {
        PaintPercentageController.Instance.OnPercentageCalculated -= SetPercentageText;
    }

    public void SetPercentageText(float percentage)
    {
        _percentageText.text = $"{percentage}%";
    }
}
