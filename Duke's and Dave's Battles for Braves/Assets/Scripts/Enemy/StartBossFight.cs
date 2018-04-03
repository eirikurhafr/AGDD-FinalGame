using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossFight : MonoBehaviour {
    private Camera mainCamera;
    public Camera bossCamera;

	// Use this for initialization
	void Start () {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Hello");
        mainCamera.enabled = false;
        bossCamera.enabled = true;
    }
}
