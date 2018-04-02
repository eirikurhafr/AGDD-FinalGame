using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip attack;
    public AudioClip jump;
    public AudioClip interact;
    public AudioClip throwObject;
    public AudioClip[] hurt;
    public AudioClip[] footSteps;
    public AudioClip pickup;
    private float footstepTimer = 0;
    public float volume;
    public float footstepVolume;

    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
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
        int number = Random.Range(0,9);
        source.PlayOneShot(hurt[number], volume);
    }

    public void playPickup()
    {
        source.PlayOneShot(pickup, volume);
    }

    public void playFootsteps()
    {
        if (footstepTimer <= 0)
        {
            int number = Random.Range(0, 10);
            source.PlayOneShot(footSteps[number], footstepVolume);
            footstepTimer = footSteps[number].length;
        }
        
    }
}
