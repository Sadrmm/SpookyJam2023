using UnityEngine;
using DG.Tweening;

public class ScaredUIComponent : MonoBehaviour
{
    private const string COLOR = "_Color";

    [SerializeField] private Material _backgroundMaterial;

    private Color32 _initialColor;
    [SerializeField] private Color _endColor;

    private void Start()
    {
        //_endColor = new Color(0.29f, 0.0204f, 0.0684f, 1);
        _initialColor = _backgroundMaterial.GetColor(COLOR);
        Hide();
    }

    public void Show(float fearDuration)
    {
        int loops = 3;
        float loopDuration = fearDuration / loops;
        gameObject.SetActive(true);
        _backgroundMaterial.DOColor(_endColor, loopDuration)
            .SetLoops(3, LoopType.Yoyo)
            .OnComplete(() => _backgroundMaterial.SetColor(COLOR, _initialColor));
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
