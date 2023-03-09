using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGenerator
{
    public static Texture2D CreateNoiseTexture (int seed, int width, int height, float noiseScale, Gradient colorGradient)
    {
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);

        Color[] colorMap = new Color[width * height];
        int i = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float noiseSample = Mathf.PerlinNoise((x + seed) / noiseScale, (y + seed) / noiseScale);
                colorMap[i] = colorGradient.Evaluate(noiseSample);
                i++;
            }
        }

        tex.filterMode = FilterMode.Bilinear;
        tex.SetPixels(colorMap);
        tex.Apply();

        return tex;
    }
}
