using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorRandomizer : CustomRandomizer
{
    [SerializeField] List<Transform> largePins;
    [SerializeField] List<Transform> smallPins;

    [Header("References")]
    [SerializeField] Transform connector;
    [SerializeField] Transform normalLargePin;
    [SerializeField] Transform[] bentLargePins;
    [SerializeField] Transform normalSmallPin;
    [SerializeField] Transform[] bentSmallPins;

    [Header("Randomization Settings")]
    [SerializeField] float bentPinAngleThreshold = 30f;
    [SerializeField] Vector2 pinRotationRange;
    [SerializeField] float bentPinPower = 5f;
    
    void PlacePins (ref List<Transform> pinList, Transform[] bentPin, Transform normalPin) 
    {
        List<Transform> resultList = new List<Transform>();
        int i = 0;
        foreach (Transform oldPin in pinList) 
        {
            // decide whether pin is bent or normal
            float bend = Mathf.Pow(Random.Range(0f, 1f), bentPinPower);
            float lerpedThreshhold = Mathf.Lerp(pinRotationRange.x, pinRotationRange.y, bend);
            Transform selectedPin = (Mathf.Abs(lerpedThreshhold) >= bentPinAngleThreshold) ? bentPin[i] : normalPin;

            // spawn pin in world, and set connector as parent
            Transform pin = Instantiate(selectedPin, oldPin.position, oldPin.rotation);
            pin.SetParent(connector);

            // set pin rotation
            Vector3 pinRot = new Vector3(lerpedThreshhold - 180f, lerpedThreshhold, lerpedThreshhold);
            pin.GetChild(0).localEulerAngles = (Random.Range(0, 2) == 0) ? pinRot : -pinRot;

            // update resultList with new object, and clean up old object
            resultList.Add(pin);
            Destroy(oldPin.gameObject);

            i++;
        }

        // set pinList to contain new objects
        pinList = resultList;
    }

    public override void Randomize () 
    {
        // place large pins
        PlacePins(ref largePins, bentLargePins, normalLargePin);

        // place small pins
        PlacePins(ref smallPins, bentSmallPins, normalSmallPin);
    }
}
