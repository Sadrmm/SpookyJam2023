using TMPro;
using UnityEngine;

public class UIMapFilledPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _percentageText;

    private void Start()
    {
        PaintPercentageController.Instance.OnPercentageCalculated += SetPercentageText;
    }

    private void Destroy()
    {
        PaintPercentageController.Instance.OnPercentageCalculated -= SetPercentageText;
    }

    public void SetPercentageText(float percentage)
    {
        _percentageText.text = $"{percentage}%";
    }
}
