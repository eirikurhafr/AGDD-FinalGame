using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordHandlerSimpleEnemy : MonoBehaviour {

    Collider collider;

    // Use this for initialization
    void Start()
    {
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
        Debug.Log(other.name);
        if (other.tag == "Player_1" || other.tag == "Player_2")
        {
            other.SendMessage("hurtFunction", 50);
        }
    }
}
