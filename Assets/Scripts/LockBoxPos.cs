using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBoxPos : MonoBehaviour
{
    public Transform HMD;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.parent);
        transform.position = new Vector3(transform.position.x, transform.parent.position.y, transform.position.z);
        transform.parent.localEulerAngles = new Vector3(0, HMD.localEulerAngles.y, 0);
    }
}
