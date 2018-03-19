using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip attack;
    public AudioClip jump;
    public AudioClip interact;
    public AudioClip throwObject;
    public AudioClip hurt;
    public AudioClip pickup;
    public float volume;

    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void playAttack()
    {
        source.PlayOneShot(attack, volume);
    }

    public void playInteract()
    {
        source.PlayOneShot(interact, volume);
    }

    public void playJump()
    {
        source.PlayOneShot(jump, volume);
    }

    public void playThrow()
    {
        source.PlayOneShot(throwObject, volume);
    }

    public void playHurt()
    {
        source.PlayOneShot(hurt, volume);
    }

    public void playPickup()
    {
        source.PlayOneShot(pickup, volume);
    }
}
