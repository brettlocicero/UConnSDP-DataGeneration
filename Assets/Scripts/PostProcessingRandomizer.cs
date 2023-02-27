using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PostProcessingRandomizer : CustomRandomizer
{
    [SerializeField] Volume volume;

    [Header("Depth of Field")]
    [SerializeField] Vector2 cameraDOFRange;
    [SerializeField] Vector2 blurRadiusRange;

    DepthOfField dof;

    void Start()
    {
        DepthOfField tmp;
        if (volume.profile.TryGet<DepthOfField>(out tmp))
        {
            dof = tmp;
        }
    }

    public override void Randomize()
    {
        float dofDistance = Random.Range(cameraDOFRange.x, cameraDOFRange.y);
        dof.focusDistance.value = dofDistance;

        float blurRadius = Random.Range(blurRadiusRange.x, blurRadiusRange.y);
        dof.farMaxBlur = blurRadius;
    }
}
