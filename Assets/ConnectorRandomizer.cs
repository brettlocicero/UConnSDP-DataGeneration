using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorRandomizer : MonoBehaviour
{
    [SerializeField] List<Transform> largePins;
    [SerializeField] List<Transform> smallPins;

    [Header("References")]
    [SerializeField] Transform connector;
    [SerializeField] Transform normalLargePin;
    [SerializeField] Transform bentLargePin;
    [SerializeField] Transform normalSmallPin;
    [SerializeField] Transform bentSmallPin;

    [Header("Randomization Settings")]
    [SerializeField] [Range(0f, 1f)] float largeBentPinChance;
    [SerializeField] [Range(0f, 1f)] float smallBentPinChance;
    
    void PlacePins (ref List<Transform> pinList, float bentChance, Transform bentPin, Transform normalPin) 
    {
        List<Transform> resultList = new List<Transform>();
        foreach (Transform oldPin in pinList) 
        {
            // decide whether pin is bent or normal
            float n = Random.Range(0f, 1f);
            Transform selectedPin = (n <= bentChance) ? bentPin : normalPin;

            // spawn pin in world, and set connector as parent
            Transform pin = Instantiate(selectedPin, oldPin.position, oldPin.rotation);
            pin.SetParent(connector);

            // update resultList with new object, and clean up old object
            resultList.Add(pin);
            Destroy(oldPin.gameObject);
        }

        // set pinList to contain new objects
        pinList = resultList;
    }

    void Randomize () 
    {
        // place large pins
        PlacePins(ref largePins, largeBentPinChance, bentLargePin, normalLargePin);

        // place small pins
        PlacePins(ref smallPins, smallBentPinChance, bentSmallPin, normalSmallPin);
    }

    void Update () 
    {
        //if (Input.GetKeyDown(KeyCode.Space)) 
            Randomize();
    }
}
