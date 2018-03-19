using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour {

    [SerializeField]
    private GameObject[] effectedGameObjects;

    private bool isActive; 

	void Start () {
	    isActive = false;
	}

	/*
	void Update () {
		
	}*/

    

    void Use() {
        isActive = !isActive;
        if (isActive) {
            foreach (GameObject x in effectedGameObjects) {
                x.SendMessage("activateObject");
            }
        } else {
            foreach (GameObject x in effectedGameObjects)
            {
                x.SendMessage("deactivateObject");
            }
        }

        foreach (GameObject g in effectedGameObjects) {
            g.SendMessage("Use");
        }
    }
}
