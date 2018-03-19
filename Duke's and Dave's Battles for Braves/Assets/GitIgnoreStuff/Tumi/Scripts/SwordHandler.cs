using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHandler : MonoBehaviour {

    Collider collider;

	// Use this for initialization
	void Start () {
        collider = GetComponent<Collider>();
    }

    private void turnOnCollider()
    {
        collider.enabled = true;
    }

    private void turnOffCollider()
    {
        collider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " og " + other.tag);
        if(other.tag == "Enemy")
        {
            other.SendMessage("hurt", 50);
        }
    }
}
