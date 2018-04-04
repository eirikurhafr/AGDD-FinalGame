﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {

    float deathTimer = 1f;
    float damage = 10;
    public AudioClip explosion;
    public float volume;
    private AudioSource source;
    private bool hitP1 = false;
    private bool hitP2 = false;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {
        source.PlayOneShot(explosion, volume);
        Destroy(gameObject, 1);
    }
	
    void Update()
    {
        if (deathTimer <= 0)
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