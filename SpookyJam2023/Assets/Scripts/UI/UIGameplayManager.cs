using UnityEngine;

public class UIGameplayManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] PlayerController _playerController;
    //[SerializeField] PlayerHabilities _playerHabilities;

    [Header("Panels")]
    [SerializeField] UIHealthBarPanel _uiHealthBarPanel;

    private void OnEnable()
    {
        _playerController.OnHealthChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        _playerController.OnHealthChanged -= UpdateHealth;
    }

    private void UpdateHealth(int newHealth)
    {
        _uiHealthBarPanel.UpdateHealth(newHealth, _playerController.StatsSO.MaxHealth);
    }
}
