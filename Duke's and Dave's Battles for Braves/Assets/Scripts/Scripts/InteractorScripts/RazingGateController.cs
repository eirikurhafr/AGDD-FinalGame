using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;

public class RazingGateController : MonoBehaviour {

	// Use this for initialization

    private Vector3 dStart;
    private Transform door;
    private bool active;
    private bool isOpen;
    private bool close;

    private

    void Start () {
        dStart = gameObject.transform.position;
        door = gameObject.transform;
    }
	
	// Update is called once per frame
	void Update () {
	    if (active) {
	        float translation = Time.deltaTime * 2f;

            if (door.position.y <= dStart.y + 4f) {
	            door.Translate(0f, translation, 0f);
	        } else {
	            active = false;
            }
        }
	    if (close) {
	        float translation2 = -Time.deltaTime * 2f;

            if (door.position.y >= dStart.y)
	        {
	            door.Translate(0f, translation2, 0f);
	        }
	        else
	        {
	            close = false;
	        }
        }
    }

    void Use() {
        if (!isOpen) {
            active = true;
        } else {
            close = true;
        }
    }
}
