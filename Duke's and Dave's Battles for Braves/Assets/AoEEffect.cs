using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEEffect : MonoBehaviour {
    Collider collider;
    float deathTimer = 1f;
    float damage = 10;
    private bool hitP1 = false;
    private bool hitP2 = false;
    // Use this for initialization
    void Start () {
        collider = GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update () {
        if(deathTimer <= 0)
        {
            Destroy(gameObject);
        }
        deathTimer -= Time.deltaTime;
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
