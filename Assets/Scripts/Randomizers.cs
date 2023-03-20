using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizers : MonoBehaviour
{
    [SerializeField] bool randomizeEveryFrame = false;
    [SerializeField] RandomizerContainer[] randomizers;

    void Update ()
    {
        // update all custom randomizers at the same time
        if (randomizeEveryFrame)
        {
            CallRandomizers();
        }

        else if (Input.GetKeyDown(KeyCode.Space) && !randomizeEveryFrame)
        {
            CallRandomizers();
        }
    }

    void CallRandomizers ()
    {
        foreach (RandomizerContainer container in randomizers)
            if (container.enabled) 
                container.customRandomizer.Randomize();
    }
}

[System.Serializable]
struct RandomizerContainer
{
    public bool enabled;
    public CustomRandomizer customRandomizer;
}
