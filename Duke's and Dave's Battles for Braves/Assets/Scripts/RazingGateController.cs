using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazingGateController : MonoBehaviour {

	// Use this for initialization

    private Vector3 dStart;
    private Transform door;
    private bool active;

    private

    void Start () {
        dStart = gameObject.transform.position;
        door = gameObject.transform;
    }
	
	// Update is called once per frame
	void Update () {
	    float translation = Time.deltaTime * 2f;
	    if (active) {
	        if (door.position.y <= dStart.y + 4f) {
	            door.Translate(0f, translation, 0f);
	        } else {
	            active = false;
            }
        }
    }

    void Use() {
        active = true;
    }
}
