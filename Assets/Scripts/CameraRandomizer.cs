using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*************************************************************************

This randomizer will vary the camera position and rotation each frame. The goal
of this randomizer is to prepare the training model for many different camera angles.

**************************************************************************/

public class CameraRandomizer : CustomRandomizer
{
    [SerializeField] Transform target;
    [SerializeField] Transform camTransform;

    [Header("Randomization Settings")]
    [SerializeField] Vector2 cameraDistanceRange;
    [SerializeField] Vector2 cameraJiggleRange;
    [SerializeField] Vector2 cameraArcRange;

    // get random point based on direction and angle
    Vector3 GetVectorOnUnitSphere (Quaternion targetDirection, float angle)
    {
        float angleInRad = Random.Range(0.0f, angle) * Mathf.Deg2Rad;
        Vector2 pointOnCircle = (Random.insideUnitCircle.normalized) * Mathf.Sin(angleInRad);
        Vector3 vec = new Vector3(pointOnCircle.x, pointOnCircle.y, Mathf.Cos(angleInRad));

        return targetDirection * vec;
    }

    // create a jiggle vector based on random x, y, z
    Vector3 JiggleVector ()
    {
        float x = Random.Range(cameraJiggleRange.x, cameraJiggleRange.y);
        float y = Random.Range(cameraJiggleRange.x, cameraJiggleRange.y);
        float z = Random.Range(cameraJiggleRange.x, cameraJiggleRange.y);

        return new Vector3(x, y, z);
    }

    // orient the camera by a distance range, arc range, and jiggle amount
    void OrientCamera ()
    {
        float cameraDistance = Random.Range(cameraDistanceRange.x, cameraDistanceRange.y);
        float camArc = Random.Range(cameraArcRange.x, cameraArcRange.y);
        camTransform.position = target.position + GetVectorOnUnitSphere(target.rotation, camArc) * cameraDistance;

        camTransform.LookAt(target.position + JiggleVector());
    }

    public override void Randomize ()
    {
        OrientCamera();
    }
}
