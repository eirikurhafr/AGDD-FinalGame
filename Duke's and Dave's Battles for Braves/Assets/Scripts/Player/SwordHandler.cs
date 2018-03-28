using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHandler : MonoBehaviour {

    Collider collider;
    private List<string> namesHit;


	// Use this for initialization
	void Start () {
        namesHit = new List<string>();
        collider = GetComponent<Collider>();
    }

    private void turnOnCollider()
    {
        namesHit.Clear();
        collider.enabled = true;
    }

    private void turnOffCollider()
    {
        collider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !namesHit.Contains(other.name))
        {
            float temp = Random.Range(18, 23);
            other.SendMessage("hurt", temp);
            namesHit.Add(other.name);
        }
    }
}
