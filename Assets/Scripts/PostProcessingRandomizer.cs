using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

/*************************************************************************

This randomizer will vary the post processing of the scene. The goal of this 
randomizer is to add even more variance through Bloom, Depth of Field, Vignetting, 
and many other that could eventually be implemented.

Depth of Field was the only post processing implemented and is meant 
to serve as a proof of concept.

**************************************************************************/

public class PostProcessingRandomizer : CustomRandomizer
{
    [SerializeField] Volume volume;

    [Header("Depth of Field")]
    [SerializeField] Vector2 cameraDOFRange;
    [SerializeField] Vector2 blurRadiusRange;

    DepthOfField dof;

    void Start()
    {
        // grab reference of Depth of Field
        DepthOfField tmp;
        if (volume.profile.TryGet<DepthOfField>(out tmp))
            dof = tmp;
    }

    public override void Randomize()
    {
        // randomize DOF distance where blur begins
        float dofDistance = Random.Range(cameraDOFRange.x, cameraDOFRange.y);
        dof.focusDistance.value = dofDistance;

        // randomize blur radius (more blur radius => higher blur)
        float blurRadius = Random.Range(blurRadiusRange.x, blurRadiusRange.y);
        dof.farMaxBlur = blurRadius;
    }
}
