using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*************************************************************************

This randomizer will vary the environmental lighting of the scene. The goal of 
this randomizer is to prepare the training model for many different lighting situations
such as darker rooms, colored lighting, or sunsets.

**************************************************************************/

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
        // randomize rotation of light
        float x = Random.Range(xAxisRotationRange.x, xAxisRotationRange.y);
        float y = Random.Range(yAxisRotationRange.x, yAxisRotationRange.y);
        directionalLight.transform.eulerAngles = new Vector3(x, y, 0f);

        // randomize color if turned on
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
        // HSV is simpler to work with than RGB for these parameters
        float hue = Random.Range(hueRange.x, hueRange.y);
        float saturation = Random.Range(saturationRange.x, saturationRange.y);
        float value = Random.Range(valueRange.x, valueRange.y);

        Color hsvColor = Color.HSVToRGB(hue, saturation, value);

        return hsvColor;
    }
}