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
    private Vector3 StartingPosition;

    // Use this for initialization
    void Start () {
        StartingPosition = this.transform.position;
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
            print("1" + cube.transform.position);
            cube.transform.position = StartingPosition;//new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.y);
            print("2" + cube.transform.position);
            cube.AddComponent<Rigidbody>();
            cube.gameObject.GetComponent<BoxCollider>().isTrigger = true;

            cube.gameObject.GetComponent<Renderer>().material = material;
           // cube.gameObject.AddComponent<waterblobBehaviour>();
            waterAmmount = 0f;
        }
    }
}
