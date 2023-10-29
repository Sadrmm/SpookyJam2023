using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditorInternal;
using System;

public class ScaredUI : MonoBehaviour
{

    [SerializeField] private Material _backgroundMaterial;

    private Color32 _initialColor;
    private Color _endColor;

    private void Start()
    {
        _endColor = new Color(0.29f, 0.0204f, 0.0684f, 1);
        _initialColor = _backgroundMaterial.GetColor("_Color");
        Show(2);
    }

    public void Show(float fearDuration)
    {
        int loops = 3;
        float loopDuration = fearDuration / loops;
        gameObject.SetActive(true);
        _backgroundMaterial.DOColor(_endColor, loopDuration).SetLoops(3, LoopType.Yoyo);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
