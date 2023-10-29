using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PaintPercentageController : MonoBehaviour
{
    private float _percentage;
    private Paintable _paintable;
    [SerializeField] private float _timer = 2;

    public static PaintPercentageController Instance;

    public event Action<float> OnPercentageCalculated;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _paintable = GetComponent<Paintable>();
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timer);
            ThreadStart threadStart = delegate
            {
                CalculatePaintedPercentage(_paintable);
            };
            threadStart.Invoke();
        }
    }

    private void CalculatePaintedPercentage(Paintable p)
    {
        RenderTexture mask = p.getMask();

        int maskPixelNumber = mask.width * mask.height;
        Color[] textureColors = GetPixels(p);

        int targetColorPixels = 0;
        foreach (Color c in textureColors)
        {
            if (c.a > 0.6)
            {
                targetColorPixels++;
            }
        }
        _percentage = Mathf.InverseLerp(0, maskPixelNumber - maskPixelNumber * (1 - GameManager.Instance.percentageToWin), targetColorPixels);

       // Debug.Log("PERCENTAGE: " + _percentage);

        OnPercentageCalculated?.Invoke(_percentage);
    }

    public static Color[] GetPixels(Paintable p)
    {
        RenderTexture mask = p.getMask();

        Texture2D temp = new Texture2D(mask.width, mask.height, TextureFormat.RGBAFloat, false);
        var oldRT = RenderTexture.active;
        RenderTexture.active = mask;
        temp.ReadPixels(new Rect(0, 0, temp.width, temp.height), 0, 0, false);
        temp.Apply();
        RenderTexture.active = oldRT;
        Color[] pixels = temp.GetPixels();
        Destroy(temp);
        return pixels;
    }
}
