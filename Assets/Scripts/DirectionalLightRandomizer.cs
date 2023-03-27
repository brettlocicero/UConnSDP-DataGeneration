using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;

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
    [SerializeField] Vector2 hueRange;
    [SerializeField] Vector2 saturationRange;
    [SerializeField] Vector2 valueRange;

    public Color GenerateColor ()
    {
        float hue = Random.Range(hueRange.x, hueRange.y);
        float saturation = Random.Range(saturationRange.x, saturationRange.y);
        float value = Random.Range(valueRange.x, valueRange.y);

        Color hsvColor = Color.HSVToRGB(hue, saturation, value);

        return hsvColor;
    }
}