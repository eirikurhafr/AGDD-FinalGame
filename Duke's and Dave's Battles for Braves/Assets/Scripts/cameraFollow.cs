using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

	private const float Y_ANGLE_MIN = 0.0f;
	private const float Y_ANGLE_MAX = 50.0f;

	public Transform lookAt;
	public Transform camTransform;
	private Camera cam;

	private float distance = 10.0f;
	private float currentX;
	private float currentY;
	private float sensitivityX = 4.0f;
	private float sensitivityY = 1.0f;
	
	// Use this for initialization
	void Start () 
	{
		camTransform = transform;
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentX += Input.GetAxis("Mouse X");
		currentY += Input.GetAxis("Mouse Y");
	}

	void LateUpdate()
	{
		Vector3 dir = new Vector3(0, 0, -distance);
		Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
		camTransform.position = lookAt.position + rotation * dir;
		camTransform.LookAt(lookAt.position);
	}
}
