using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterblobBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (gameObjcet.position.y <= -20f) {
	        destroy(gameObjcet);
	    }
	}

    void OnTriggerEnter(Collider other) {
        other.sendMessage("makeWet");
    }

}
