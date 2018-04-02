using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtColliderScript : MonoBehaviour {

    
    [SerializeField] GameObject BadDude;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Rigidbody>().velocity.y < 0)
        {
            BadDude.SendMessage("hurt", 100);
        }
    }
}
