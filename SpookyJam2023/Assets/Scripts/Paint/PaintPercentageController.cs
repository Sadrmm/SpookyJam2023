using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PaintPercentageController : MonoBehaviour
{
    public float Percentage { get; private set; }
    private Paintable _paintable;
    [SerializeField] private float _timer = 2;

    public event Action OnPercentageCalculated;


    public static PaintPercentageController Instance;

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
            if (c == Color.black)
            {
                targetColorPixels++;
            }
        }
        Percentage = Mathf.InverseLerp(0, maskPixelNumber, targetColorPixels);

        Debug.Log(Percentage);

        OnPercentageCalculated?.Invoke();
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
        return pixels;
    }
}
