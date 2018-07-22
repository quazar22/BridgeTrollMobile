using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	// this is the transform used as a reference to move the camera
	public Transform reference;
	//  these are the factors used to change the speed in which the camera follows the plane
	public float posF,rotF;
    private Vector3 offset;

	void Start ()
    {
        offset = new Vector3(0, 12, -24);
        reference = GameObject.Find("ClientPlayer").transform;
	}
	
	void FixedUpdate () 
	{
        // we use the lerp for changing the position and rotation of the camera
        //only change x, z movement, never y
        transform.position = reference.position + offset;
        //transform.position = Vector3.Lerp(transform.position, reference.position, posF);
        //transform.rotation = Quaternion.Lerp(transform.rotation, reference.rotation, rotF);
    }
}
