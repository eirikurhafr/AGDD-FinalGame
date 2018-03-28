using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
    public Vector3 location;

    void OnCollisionEnter(Collision theCollision)
    {
        Debug.Log("Am doing something");
        theCollision.gameObject.SendMessage("Respawn", location);
    }
}
