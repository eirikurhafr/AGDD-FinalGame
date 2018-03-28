using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowNPCScript : MonoBehaviour {
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
	}

    void StopNPC ()
    {
        rb.isKinematic = true;
    }

    private void Respawn(Vector3 location)
    {
        gameObject.transform.position = location;
    }
}
