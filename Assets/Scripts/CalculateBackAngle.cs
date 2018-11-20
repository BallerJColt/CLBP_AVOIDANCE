using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateBackAngle : MonoBehaviour {

	public Transform hipTracker;
	public Transform backTracker;
	public float backRotationAngle;
	public Vector3 asf;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion hipRotQ = hipTracker.localRotation;
		Quaternion backRotQ = backTracker.localRotation;
		Quaternion rotationDifference = backRotQ * Quaternion.Inverse(hipRotQ);
		asf = rotationDifference.eulerAngles;
	}
}
