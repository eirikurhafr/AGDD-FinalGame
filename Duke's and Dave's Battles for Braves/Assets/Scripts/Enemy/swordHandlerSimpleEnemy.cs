
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordHandlerSimpleEnemy : MonoBehaviour {

    public float damage;
    private bool hitP1 = false;
    private bool hitP2 = false;
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
        hitP1 = false;
        hitP2 = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_1" && !hitP1)
        {
            hitP1 = true;
            other.SendMessage("hurtFunction", damage);
        }
        else if (other.tag == "Player_2" && !hitP2)
        {
            hitP2 = true;
            other.SendMessage("hurtFunction", damage);
        }
    }
}
