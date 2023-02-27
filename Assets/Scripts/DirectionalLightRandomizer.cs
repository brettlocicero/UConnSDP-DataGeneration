using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightRandomizer : CustomRandomizer
{
    [SerializeField] Light directionalLight;

    [Header("Rotation")]
    [SerializeField] Vector2 xAxisRotationRange;
    [SerializeField] Vector2 yAxisRotationRange;

    [Header("Light Color")]
    [SerializeField] bool useRandomColors = false;
    [SerializeField] ColorRange colorRanges;

    public override void Randomize ()
    {
        float x = Random.Range(xAxisRotationRange.x, xAxisRotationRange.y);
        float y = Random.Range(yAxisRotationRange.x, yAxisRotationRange.y);
        directionalLight.transform.eulerAngles = new Vector3(x, y, 0f);

        if (useRandomColors)
            directionalLight.color = colorRanges.GenerateColor();
    }
}

[System.Serializable]
struct ColorRange
{
    [SerializeField] [Range(0f, 1f)] float possibleRed;
    [SerializeField] [Range(0f, 1f)] float possibleGreen;
    [SerializeField] [Range(0f, 1f)] float possibleBlue;
    [SerializeField] bool grayscale;

    public Color GenerateColor ()
    {
        float r = Random.Range(0f, possibleRed);
        float g = Random.Range(0f, possibleGreen);
        float b = Random.Range(0f, possibleBlue);
        float avgColor = (r + g + b) / 3f;

        return (grayscale) ? new Color(avgColor, avgColor, avgColor) : new Color(r, g, b);
    }
}