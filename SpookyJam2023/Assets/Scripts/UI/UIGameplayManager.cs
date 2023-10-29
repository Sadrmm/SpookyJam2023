using System;
using UnityEngine;

public class UIGameplayManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] PlayerController _playerController;
    //[SerializeField] PlayerHabilities _playerHabilities;

    [Header("Panels")]
    [SerializeField] UIHealthBarPanel _uiHealthBarPanel;
    [SerializeField] UIAttacksPanel _uiAttacksPanel;
    [SerializeField] UIEndGamePanel _uiEndGamePanel;

    private void Start()
    {
        _playerController.OnHealthChanged += UpdateHealth;
        GameManager.Instance.OnGameEnded += ShowEndGamePanel;

        _uiAttacksPanel.SetAttacksUI();
        _uiEndGamePanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _playerController.OnHealthChanged -= UpdateHealth;
        GameManager.Instance.OnGameEnded -= ShowEndGamePanel;
    }

    private void ShowEndGamePanel()
    {
        Debug.Log("dentro ui end");
        _uiEndGamePanel.Show();
    }
    private void UpdateHealth(int newHealth)
    {
        _uiHealthBarPanel.UpdateHealth(newHealth, _playerController.StatsSO.MaxHealth);
    }
}
