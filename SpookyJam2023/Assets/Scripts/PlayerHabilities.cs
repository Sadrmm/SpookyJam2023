using UnityEngine;

public class PlayerHabilities : MonoBehaviour
{
    [SerializeField] KeyCode _scareAttackKeyCode;
    [SerializeField] Scare _scare;

    private void Update()
    {
        if (Input.GetKeyDown(_scareAttackKeyCode)) {
            _scare.Attack();
        }
    }
}
