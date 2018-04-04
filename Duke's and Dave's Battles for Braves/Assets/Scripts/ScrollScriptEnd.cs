using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScriptEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<BoxCollider>() == null)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
	}
}
