using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobberBadDude: MonoBehaviour {

    [SerializeField]
    private float health;

    [SerializeField]
    private Transform[] targets;

    [SerializeField]
    private float range;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float speed;

    private bool isAlive;
    private Transform currTarget;
    private float distToTarget;

    private Transform transform;

    private NavMeshAgent agent;


    private float nextActionTime = 0.0f;
    public float period = 5f;


    // Use this for initialization
    void Start () {
        isAlive = true;
        transform = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        currTarget = targets[0];

    }
	
	// Update is called once per frame
	void Update () {
        agent.destination = currTarget.position;
        Debug.Log(currTarget.position);

        //Update currTarget to the closest target every 5 seconds 
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            // execute block of code here

            foreach (Transform x in targets)
            {
                if (Vector3.Distance(x.position, transform.position) < Vector3.Distance(currTarget.position, transform.position))
                {
                    currTarget = x;
                }
            }

        }
    }
}
