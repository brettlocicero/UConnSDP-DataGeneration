using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizers : MonoBehaviour
{
    [SerializeField] bool randomizeEveryFrame = false;
    [SerializeField] CustomRandomizer[] customRandomizers;

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
        foreach (CustomRandomizer randomizer in customRandomizers)
            randomizer.Randomize();
    }
}
