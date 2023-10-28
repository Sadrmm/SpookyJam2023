using UnityEngine;
using UnityEngine.UI;

public class UIHealthBarPanel : MonoBehaviour
{
    [SerializeField] Image _healthBar;

    public void UpdateHealth(int newHealth, int maxHealth)
    {
        _healthBar.fillAmount = (float) newHealth / maxHealth;
    }
}
