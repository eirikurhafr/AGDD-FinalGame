using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject t1;
    public GameObject t2;
    private PlayerController player1;
    private PlayerController player2;
    public GameObject self;
    public Camera cam;
    public Vector3 oldLoc;
    public Vector3 offsetSolo;

    // Use this for initialization
    void Start()
    {
        offsetSolo.x = -0.146084f;
        offsetSolo.y = 9.238083f;
        offsetSolo.z = -5.30064f;
        player1 = t1.GetComponent<PlayerController>();
        player2 = t2.GetComponent<PlayerController>();
        cam = self.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player1.getDead() && !player2.getDead())
        {
            FixedCameraFollowSmooth(cam, t1.transform, t2.transform);
        }
        else if (player1.getDead())
        {
            OnePlayerCamera(t2.transform);
        }
        else if (player2.getDead())
        {
            OnePlayerCamera(t1.transform);
        }
    }

    public void OnePlayerCamera(Transform player)
    {
        cam.transform.position = player.position + offsetSolo;
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
