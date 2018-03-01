using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform t1;
    public Transform t2;
    public GameObject self;
    public Camera cam;
    public Vector3 oldLoc;

    // Use this for initialization
    void Start()
    {
        cam = self.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        FixedCameraFollowSmooth(cam, t1, t2);
    }

    public void FixedCameraFollowSmooth(Camera cam, Transform t1, Transform t2)
    {
        // Offset for the normal camera location
        Vector3 offsetNormal;
        offsetNormal.x = 0;
        offsetNormal.y = 0.5f;
        offsetNormal.z = -0.4f;

        float zoomFactor = 0.8f;
        float followTimeDelta = 0.1f;

        // Midpoint we're after
        Vector3 midpoint = (t1.position + t2.position) / 2f;

        // Distance between objects
        float distance = (t1.position - t2.position).magnitude;
        

        // Move camera a certain distance
        Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;

        // You specified to use MoveTowards instead 
        cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta) + offsetNormal;

        // Snap when close enough to prevent annoying slerp behavior
        if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
            cam.transform.position = cameraDestination;
    }
}
