using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterblobBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (gameObject.transform.position.y <= -20f) {
	        Destroy(gameObject);
	    }
	}

    void OnTriggerEnter(Collider other) {
        other.SendMessage("spillWater");
    }

}
