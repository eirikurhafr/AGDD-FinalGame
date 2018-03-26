using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDamageCanvas : MonoBehaviour {
    private GameObject camera;
	// Use this for initialization
	void Start () {
        camera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(2 * transform.position - camera.transform.position);
    }
}
