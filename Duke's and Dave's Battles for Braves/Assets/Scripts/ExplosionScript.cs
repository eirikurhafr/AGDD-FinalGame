using System.Collections;
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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_1" || other.tag == "Player_2")
        {
            other.SendMessage("hurtFunction", damage);
        }
        else
        {
            other.SendMessage("Explosion");
        }
    }
}
