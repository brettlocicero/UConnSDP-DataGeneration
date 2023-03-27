using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConnectorRandomizer : CustomRandomizer
{
    [SerializeField] List<Transform> largePins;
    [SerializeField] List<Transform> smallPins;

    [Header("References")]
    [SerializeField] Transform connector;
    [SerializeField] Transform[] normalLargePins;
    [SerializeField] Transform[] bentLargePins;
    [SerializeField] Transform[] normalSmallPins;
    [SerializeField] Transform[] bentSmallPins;

    [Header("Randomization Settings")]
    [SerializeField] float bentPinAngleThreshold = 30f;
    [SerializeField] Vector2 pinRotationRange;
    [SerializeField] float bentPinPower = 5f;
    [SerializeField] Vector2 connectorRotationRange;
    
    void PlacePins (ref List<Transform> pinList, Transform[] bentPin, Transform[] normalPins) 
    {
        List<Transform> resultList = new List<Transform>();
        int i = 0;
        foreach (Transform oldPin in pinList) 
        {
            // decide whether pin is bent or normal
            float bend = Mathf.Pow(Random.Range(0f, 1f), bentPinPower);
            float lerpedThreshhold = Mathf.Lerp(pinRotationRange.x, pinRotationRange.y, bend);
            Transform selectedPin = (Mathf.Abs(lerpedThreshhold) >= bentPinAngleThreshold) ? bentPin[i] : normalPins[i];

            // spawn pin in world, and set connector as parent
            Transform pin = Instantiate(selectedPin, oldPin.position, oldPin.rotation, connector);

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

    void RandomlyRotateConnector ()
    {
        Vector3 rot = new Vector3(0f, 0f, Random.Range(connectorRotationRange.x, connectorRotationRange.y));
        connector.eulerAngles = rot;
    }

    public override void Randomize () 
    {
        // place large pins
        PlacePins(ref largePins, bentLargePins, normalLargePins);

        // place small pins
        PlacePins(ref smallPins, bentSmallPins, normalSmallPins);

        RandomlyRotateConnector();
    }
}
