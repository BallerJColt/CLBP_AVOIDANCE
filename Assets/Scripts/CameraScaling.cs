using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraScaling : MonoBehaviour
{

    public Transform HMD;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //DoNotRotate(HMD.transform.position);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, -HMD.localEulerAngles.y, transform.localEulerAngles.z);
        //transform.position = new Vector3(-HMD.localPosition.x, transform.position.y, -HMD.localPosition.z);
        //Debug.DrawLine(transform.position, HMD.position, Color.red);
        //transform.position = -HMD.localPosition;
        //Debug.Log(HMD.localPosition.z);


        //Vector3 HMDPos = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.CenterEye);
        //Quaternion HMDRot = UnityEngine.XR.InputTracking.GetLocalRotation(UnityEngine.XR.XRNode.CenterEye);
        //Debug.Log(HMDRot);
        //transform.position = -InputTracking.GetLocalPosition(XRNode.CenterEye);
        //transform.rotation = InputTracking.GetLocalRotation(XRNode.CenterEye);

        //UnityEngine.XR.InputTracking.disablePositionalTracking = true;
    }


    void LateUpdate()
    {
        float radius = Vector3.Magnitude(new Vector3(transform.position.x - HMD.position.x, 0, transform.position.z - HMD.position.z));
        //Debug.Log(radius);
        float sinDisp = Mathf.Sin(Mathf.Deg2Rad * Mathf.Abs(HMD.localEulerAngles.y));
        float cosDisp = Mathf.Cos(Mathf.Deg2Rad * Mathf.Abs(HMD.localEulerAngles.y));
        float xDisplace = sinDisp * radius;
        float zDisplace = cosDisp * radius;
        float asf = radius - zDisplace;

        //Debug.Log("angle: " + HMD.localEulerAngles.y);
        //Debug.Log("sin: " + xDisplace);
        Debug.DrawLine(HMD.position, HMD.position + Vector3.right * xDisplace, Color.green);
        Debug.DrawLine(HMD.position, HMD.position - Vector3.forward * asf, Color.green);

        //transform.position = new Vector3(xDisplace, transform.position.y, -zDisplace);
    }

}
