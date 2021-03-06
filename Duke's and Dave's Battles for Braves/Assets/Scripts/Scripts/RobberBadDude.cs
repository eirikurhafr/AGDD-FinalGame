﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobberBadDude: MonoBehaviour {

    [SerializeField]
    private float health;
    [SerializeField]
    private Transform[] targets;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float speed;

    private bool isAlive;
    private Transform currTarget;
    private float distToTarget;
    private Transform transform;
    private NavMeshAgent agent;
    private SphereCollider rangeSphere;
    private float nextActionTime = 0.0f;
    public float period = 1f;
    private float waitForDamage;

    void Start () {
        health = 100;
        isAlive = true;
        transform = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        currTarget = targets[0];
    }
	
	void Update () {
        if(Vector3.Distance( currTarget.position, transform.position) >= 0.8f) {
            agent.destination = currTarget.position;
        }

        if(health <= 0f){
            Debug.Log("dead");
            Destroy(gameObject);
        }

        //Update currTarget to the closest target every 5 seconds 
        if (Time.time > nextActionTime) {
            nextActionTime += period;
            foreach (Transform x in targets) {
                if (Vector3.Distance(x.position, transform.position) < Vector3.Distance(currTarget.position, transform.position)) {
                    Debug.Log("Changed Target");
                    currTarget = x;
                }
            }
        }
        waitForDamage -= Time.deltaTime;
    }

    private void hurt(float damage)
    {
        health -= damage;
        if(health < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other){
        var foreignObject = other.gameObject.tag == "Player_1" ? "Player_1" : (other.gameObject.tag == "Player_2" ? "Player_2" : "null");
        if (other.gameObject.tag == foreignObject && waitForDamage <= 0 && !(other.transform.position.y >= transform.position.y + 2)) {
            GameObject playerHurt = new GameObject();
            if (other.gameObject.name == targets[0].name) {
                Debug.Log("Damage: " + targets[0].name);
                targets[0].SendMessage("hurtFunction", damage);
            }
            else if(other.gameObject.name == targets[1].name) {
                Debug.Log("Damage: " + targets[1].name);
                targets[1].SendMessage("hurtFunction", damage);
            }
            waitForDamage = 2f;
        }
        else if (other.gameObject.tag == foreignObject && (other.transform.position.y >= transform.position.y + 2))
        {
            health = 0;
            Debug.Log("ahhhhh" + health);
        }

        if (other.gameObject.tag == "Throwable" ) {
            if(other.GetComponent<Rigidbody>().velocity.magnitude >= 1f) {
                health = 0;
                Debug.Log("ahhhhh" + health);
            }
        }
    }



}
