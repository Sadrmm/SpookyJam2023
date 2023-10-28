using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PaintPercentageController
{
    private static float _percentage;

    public static event Action OnPercentageCalculated;

    public static void CalculatePaintedPercentage(Paintable p)
    {
        RenderTexture mask = p.getMask();

        int maskPixelNumber = mask.width * mask.height;
        Color[] textureColors = getPixels(p);

        int redPixels = 0;
        foreach (Color c in textureColors)
        {
            if (c == Color.red)
            {
                redPixels++;
            }
        }

        Debug.Log("RED PIXELS: " + redPixels);
        Debug.Log("MASK PIXELS: " + maskPixelNumber);

        _percentage = Mathf.InverseLerp(0, maskPixelNumber, redPixels);

        OnPercentageCalculated?.Invoke();

        Debug.Log("PERCENTAGE OF RED PIXELS: " + _percentage);
    }

    public static Color[] getPixels(Paintable p)
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
