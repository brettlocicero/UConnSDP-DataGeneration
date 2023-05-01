using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*************************************************************************

This master object will control all the randomizers that will participate
in the scene. Either every frame or when pressing space all randomizers will
be called to generate a unique image.

**************************************************************************/


public class Randomizers : MonoBehaviour
{
    [SerializeField] bool randomizeEveryFrame = false;
    [SerializeField] RandomizerContainer[] randomizers;

    void Update ()
    {
        // update all custom randomizers at the same time
        if (randomizeEveryFrame)
            CallRandomizers();
        
        // for testing purposes, can press space to call Randomizers
        else if (Input.GetKeyDown(KeyCode.Space) && !randomizeEveryFrame)
            CallRandomizers();
    }

    void CallRandomizers ()
    {
        // loop through all randomizers and call Randomize() on those enabled
        foreach (RandomizerContainer container in randomizers)
            if (container.enabled) 
                container.customRandomizer.Randomize();
    }
}

// struct allows enabling/disabling of CustomRandomizers without 
// them having to be removed/readded from array in inspector
[System.Serializable]
struct RandomizerContainer
{
    public bool enabled;
    public CustomRandomizer customRandomizer;
}
