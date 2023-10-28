using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAttacksPanelItem : MonoBehaviour
{
    [SerializeField] Image _attackImg;
    [SerializeField] Image _attackOffFilter;

    [SerializeField] TextMeshProUGUI _keyText;
    [SerializeField] Image _keyOffFilter;

    private HabilityStatsSO _attackStatsSO;

    private void OnEnable()
    {
        PlayerHabilities.OnTimerUpdated += UpdateOffFilter;
    }

    private void OnDisable()
    {
        PlayerHabilities.OnTimerUpdated -= UpdateOffFilter;
    }
    
    private void UpdateOffFilter(float timeNormalized, HabilityStatsSO habilityStatsSO)
    {
        if (habilityStatsSO != _attackStatsSO) {
            return;
        }

        float coolDown = habilityStatsSO.Cooldown;
        timeNormalized = Mathf.Min(timeNormalized, coolDown);

        float fillAmount = 1 - timeNormalized / coolDown;
        _attackOffFilter.fillAmount = fillAmount;
        _keyOffFilter.fillAmount = fillAmount;

        //Debug.Log($"TimeNormalized: {timeNormalized}, Cooldown: {coolDown}, FillAmount: {fillAmount}");
    }

    public void InitItem(Sprite attackSprite, KeyCode key, HabilityStatsSO statsSO)
    {
        _attackImg.sprite = attackSprite;

        string keyString = key == KeyCode.None ? "AUTO" : key.ToString();
        _keyText.text = keyString;

        _attackStatsSO = statsSO;
    }
}
