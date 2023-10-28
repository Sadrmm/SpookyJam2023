using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAttacksPanelItem : MonoBehaviour
{
    [SerializeField] Image _attackImg;
    [SerializeField] Image _attackOffFilter;

    [SerializeField] TextMeshProUGUI _keyText;
    [SerializeField] Image _keyOffFilter;

    public void SetItem(Sprite attackSprite, KeyCode key)
    {
        _attackImg.sprite = attackSprite;

        string keyString = key == KeyCode.None ? string.Empty : key.ToString();
        _keyText.text = keyString;
    }
}
