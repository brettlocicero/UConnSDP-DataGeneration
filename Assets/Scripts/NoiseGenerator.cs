using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGenerator
{
    public static Texture2D CreateNoiseTexture (int seed, int width, int height, float noiseScale, Gradient colorGradient)
    {
        // new texture that has 32 bits for RGBA
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);

        // color map size is width * height
        Color[] colorMap = new Color[width * height];

        int i = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // sample noise at seed and evaluate gradient based on noise value
                float noiseSample = Mathf.PerlinNoise((x + seed) / noiseScale, (y + seed) / noiseScale);
                colorMap[i] = colorGradient.Evaluate(noiseSample);
                i++;
            }
        }

        // texture settings for Unity
        tex.filterMode = FilterMode.Bilinear;
        tex.SetPixels(colorMap);
        tex.Apply();

        return tex;
    }
}
