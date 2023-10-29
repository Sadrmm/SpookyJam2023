using TMPro;
using UnityEngine;

public class UITimePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerText;

    private void Start()
    {
        GameManager.Instance.OnTimerUpdated += SetTimerText;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnTimerUpdated -= SetTimerText;
    }

    public void SetTimerText(float timer)
    {
        string timerString = timer.ToString("F0");
        _timerText.text = timerString;
    }
}
