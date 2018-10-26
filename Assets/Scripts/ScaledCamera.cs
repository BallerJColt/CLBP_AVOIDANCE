using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScaledCamera : MonoBehaviour
{
    public Transform HMD;
    //public float positionMultiplier = 1f;
    [Range(1, 2)]
    public float rotationMultiplier = 1f;
    public bool scalingEnabled = false;
    Quaternion referenceRotation;
    public Vector3 referenceRotationEuler;
    public Vector3 globalReferenceRotationEuler;
    public Vector3 HMDRotationEuler;
    public Vector3 globalHMDRotation;
    public Vector3 rotationDifference;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        #region Debug key inputs
        if (Input.GetKeyUp("space"))
        {
            CalibrateRotation();
        }

        if (Input.GetKeyUp("b"))
        {
            scalingEnabled = !scalingEnabled;
        }
        #endregion
        //transform.localPosition = HMD.localPosition * -1f;
        HMDRotationEuler = HMD.transform.localRotation.eulerAngles;
        globalHMDRotation = HMD.transform.eulerAngles;
        if (referenceRotation != null && referenceRotationEuler != null)
        {
            rotationDifference = SignedAngleDifference(AbsoluteAngleDifference(SubtractByElement(HMDRotationEuler, referenceRotationEuler)));
        }


        if (scalingEnabled)
        {
            //transform.rotation = Quaternion.LerpUnclamped(Quaternion.identity, Quaternion.LookRotation(HMD.forward), rotationMultiplier - 1f);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationDifference.y * (rotationMultiplier - 1f), transform.localEulerAngles.z);
        }
    }

    void ResetPosition()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    void CalibrateRotation()
    {
        //scalingEnabled = false;
        referenceRotation = HMD.transform.localRotation;
        referenceRotationEuler = referenceRotation.eulerAngles;
        globalReferenceRotationEuler = HMD.transform.eulerAngles;
        ResetPosition();
    }

    //a-b by element
    Vector3 SubtractByElement(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    Vector3 AbsoluteAngleDifference(Vector3 a)
    {
        Vector3 b = new Vector3();
        if (a.x < 0) b.x = 360 + a.x;
        else b.x = a.x;
        if (a.y < 0) b.y = 360 + a.y;
        else b.y = a.y;
        if (a.z < 0) b.z = 360 + a.z;
        else b.z = a.z;

        return b;
    }

    Vector3 SignedAngleDifference(Vector3 a)
    {
        Vector3 b = new Vector3();
        if (a.x > 180) b.x = a.x - 360;
        else b.x = a.x;
        if (a.y > 180) b.y = a.y - 360;
        else b.y = a.y;
        if (a.z > 180) b.z = a.z - 360;
        else b.z = a.z;

        return b;
    }

    Vector3 AngleOverflow(Vector3 a)
    {
        return new Vector3(a.x % 360, a.y % 360, a.z % 360);
    }

}
