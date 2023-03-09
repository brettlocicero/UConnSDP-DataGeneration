using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionRandomizer : CustomRandomizer
{
    [SerializeField] Transform reflectionQuad;

    [Header("Transform Settings")]
    [SerializeField] Transform target;
    [SerializeField] Vector2 possibleDistance;
    [SerializeField] Vector2 xScaleRange;
    [SerializeField] Vector2 yScaleRange;
    [SerializeField] Vector2 xPosRange;
    [SerializeField] Vector2 yPosRange;

    [Header("Noise Settings")]
    [SerializeField] Texture2D generatedTexture;
    [SerializeField] int seed;
    [SerializeField] int width = 128;
    [SerializeField] int height = 128;
    [SerializeField] float noiseScale;
    [SerializeField] Gradient[] colorGradients;

    public override void Randomize ()
    {
        float dist = Random.Range(possibleDistance.x, possibleDistance.y);
        float xScale = Random.Range(xScaleRange.x, xScaleRange.y);
        float yScale = Random.Range(yScaleRange.x, yScaleRange.y);
        float xPos = Random.Range(xPosRange.x, xPosRange.y);
        float yPos = Random.Range(yPosRange.x, yPosRange.y);

        Vector3 pos = new Vector3(xPos, yPos, dist);
        Vector3 size = new Vector3(xScale, yScale, 1f);
        reflectionQuad.position = pos;
        reflectionQuad.localScale = size;
        reflectionQuad.LookAt(target);

        seed = Random.Range(-10000, 10000);
        generatedTexture = NoiseGenerator.CreateNoiseTexture(seed, width, height, noiseScale, colorGradients[Random.Range(0, colorGradients.Length)]);
        reflectionQuad.GetComponent<MeshRenderer>().material.SetTexture("_BaseColorMap", generatedTexture);
    }
}
