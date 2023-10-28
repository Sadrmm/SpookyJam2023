using UnityEngine;

public class UIAttacksPanel : MonoBehaviour
{
    [SerializeField] Transform _attacksContainer;
    [SerializeField] GameObject _attackTemplate;

    [SerializeField] HabilityStatsSO[] _habilitiesStatsSO;

    public void SetAttacksUI()
    {
        foreach (HabilityStatsSO hability in _habilitiesStatsSO)
        {
            GameObject attackGO = Instantiate(_attackTemplate, _attacksContainer);
            UIAttacksPanelItem attackItem = attackGO.GetComponent<UIAttacksPanelItem>();

            if (attackItem == null) {
                Debug.LogError($"UIAttackItem does not have {nameof(UIAttacksPanelItem)}");
                return;
            }

            attackItem.InitItem(hability.Sprite, hability.KeyCode, hability);
        }

        _attackTemplate.SetActive(false);
    }
}
