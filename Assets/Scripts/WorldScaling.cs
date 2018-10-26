using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Axis { Pitch, Yaw, Roll };

public class WorldScaling : MonoBehaviour
{
    public Transform HMD;
    public float yRotation;
    [Range(-10, 10)]
    public int multiplicationSteps;
    public float rotationMultiplier;
    public float scaledRotation;
    public bool scalingEnabled;
    Vector3 HMDtoOrigin;


    public Axis scaleAxis;

    // Use this for initialization
    void Start()
    {

    }

    void LateUpdate()
    {
        if (Input.GetKeyUp("right"))
        {
            scalingEnabled = false;
            multiplicationSteps++;
        }
        if (Input.GetKeyUp("left"))
        {
            scalingEnabled = false;
            multiplicationSteps--;
        }
        if (Input.GetKeyUp("space"))
        {
            scalingEnabled = !scalingEnabled;
        }
        rotationMultiplier = multiplicationSteps * -0.02f;
        if (rotationMultiplier < 0.11f && rotationMultiplier > 0.09f) rotationMultiplier = 0.1f;
        if (rotationMultiplier > -0.11f && rotationMultiplier < -0.09f) rotationMultiplier = -0.1f;

        if (scalingEnabled)
        {
            /* //old implementation that only works with yaw
            Vector3 originToHMD = new Vector3(HMD.position.x, 0, HMD.position.z);
            yRotation = HMD.localEulerAngles.y;
            if (yRotation > 180f)
            {
                yRotation -= 360f;
            }
            Quaternion rot = Quaternion.AngleAxis(yRotation * rotationMultiplier, Vector3.up);
            HMDtoOrigin = rot * originToHMD;
            scaledRotation = yRotation * rotationMultiplier;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, scaledRotation, transform.localEulerAngles.z);
            Debug.DrawLine(transform.position, transform.position + originToHMD, Color.green);
            Debug.DrawLine(HMD.position, HMD.position - HMDtoOrigin, Color.red);
            transform.position = new Vector3(HMD.position.x - HMDtoOrigin.x, 0, HMD.position.z - HMDtoOrigin.z); */

            //new cool implementation
            float foo = GetTrackedHMDAngle(scaleAxis);
            ScaleRotation(foo, rotationMultiplier, scaleAxis);
        }
        else
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
    }

    void ScaleRotation(float trackedAngle, float multiplier, Axis axis)
    {
        Vector3 originToHMD = new Vector3(HMD.position.x, 0, HMD.position.z);
        float angle = trackedAngle;
        if (angle > 180f)
        {
            angle -= 360f;
        }
        Quaternion rot = Quaternion.AngleAxis(angle * multiplier, ChooseAxis(axis));
        HMDtoOrigin = rot * originToHMD;
        scaledRotation = angle * multiplier;
        transform.localEulerAngles = MakeScaledRotation(scaledRotation, axis);
        transform.position = MakeScaledTranslation(HMD.position - HMDtoOrigin, axis);

    }

    Vector3 ChooseAxis(Axis axis)
    {
        if (axis == Axis.Pitch) return Vector3.right;
        else if (axis == Axis.Yaw) return Vector3.up;
        else return Vector3.forward;
    }

    float GetTrackedHMDAngle(Axis axis)
    {
        return HMD.localEulerAngles[(int)axis];
    }

    Vector3 MakeScaledRotation(float angle, Axis axis)
    {
        Vector3 rotatedEulerAngles = transform.localEulerAngles;
        rotatedEulerAngles[(int)axis] = angle;

        return rotatedEulerAngles;
    }

    Vector3 MakeScaledTranslation(Vector3 translation, Axis axis)
    {
        Vector3 scaledTranslation = translation;
        scaledTranslation[(int)axis] = 0;
        //scaledTranslation.y -= HMD.position.y;
        return scaledTranslation;
    }
}