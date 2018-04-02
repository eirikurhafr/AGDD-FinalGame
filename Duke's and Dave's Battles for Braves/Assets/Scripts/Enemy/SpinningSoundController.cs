using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSoundController : MonoBehaviour {

    public AudioClip hellicopter;
    public float volume;
    private AudioSource source;
    
    void Start () {
        source = GetComponent<AudioSource>();
        source.pitch = (float)1.25;
    }

    public void playHellicopter()
    {
        source.PlayOneShot(hellicopter, volume);
    }

    public float getHeliLength()
    {
        return hellicopter.length;
    }
}
