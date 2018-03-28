using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDumper : MonoBehaviour {

    [SerializeField]
    private float waterAmmount;

    [SerializeField]
    private float refillRate;

    [SerializeField]
    private Material material;


	// Use this for initialization
	void Start () {
	    waterAmmount = 100f;
	    if (refillRate == 0f) {
	        refillRate = 12f;
	    }
	}
	
	// Update is called once per frame
	void Update () {
	    if (waterAmmount <= 100f) {
	        waterAmmount += Time.deltaTime * refillRate;
        }
    }

    void Use() {
        if (waterAmmount >= 100f) {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.AddComponent<Rigidbody>();
            cube.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
            cube.gameObject.GetComponent<Renderer>().material = material;
           // cube.gameObject.AddComponent<waterblobBehaviour>();
            waterAmmount = 0f;
        }
    }
}
