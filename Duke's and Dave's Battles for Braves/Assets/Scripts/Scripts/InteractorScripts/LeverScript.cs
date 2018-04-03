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
   

    void Use() {
        foreach (GameObject g in effectedGameObjects) {
            g.SendMessage("Use");
        }
    }
}
