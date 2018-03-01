using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody rb;
    public float speed;
    public string forwardKey;
    public string backwardsKey;
    public string leftKey;
    public string rightKey;

    // Use this for initialization
    void Start () {
        
        rb = GetComponent<Rigidbody>();
    }

    void PlayerInput()
    {
        if (Input.GetKey(rightKey))
        {
            rb.AddForce(speed, 0, 0, ForceMode.Impulse);
        }
        if (Input.GetKey(leftKey))
        {
            rb.AddForce(-speed, 0, 0, ForceMode.Impulse);
        }
        if (Input.GetKey(forwardKey))
        {
            rb.AddForce(0, 0, speed, ForceMode.Impulse);
        }
        if (Input.GetKey(backwardsKey))
        {
            rb.AddForce(0, 0, -speed, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update () {
        PlayerInput();
    }
}
