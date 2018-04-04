using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollScript : MonoBehaviour {

    GameObject ChildGameObject1;
    GameObject ChildGameObject2;

    // Use this for initialization
    void Start () {
        ChildGameObject1 = gameObject.transform.GetChild(0).gameObject;
        ChildGameObject2 = gameObject.transform.GetChild(1).gameObject;
    }
	
	// Update is called once per frame
	void Update () {

    }

}
