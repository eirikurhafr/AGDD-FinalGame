using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	// Use this for initialization

    private Transform door1;
    private Transform door2;
    private Vector3 d1Start;
    private Vector3 d2Start;
    private bool active;

    private

    void Start () {
	    bool first = true;
	    foreach (Transform child in transform)
	    {
	        if (first) {
	            door1 = child;
	            first = !first;
	            d1Start = child.position;
	        } else {
	            door2 = child;
	            d2Start = child.position;
	        }
	    }
        active = false;
    }
	
	// Update is called once per frame
	void Update () {
	    float translation = Time.deltaTime * 2;
	    if (active) {
	        if (door1.position.z >= d1Start.z - 2f) {
	            door1.Translate(0, 0, -translation);
	            door2.Translate(0, 0, translation);
            } else {
	            active = false;
            }
        }
    }

    void Use() {
        active = true;
    }
}
