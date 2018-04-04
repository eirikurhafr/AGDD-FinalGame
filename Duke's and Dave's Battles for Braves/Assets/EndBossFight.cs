using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossFight : MonoBehaviour {
    public waterBoi boss;
    public GameObject[] activates;
    private GameObject music;
    public GameObject bossMusic;

    private void Start()
    {
        music = GameObject.Find("Music");
    }

    private void Update()
    {
        if(boss.health == 0)
        {
            Go();
            ChangeMusic();
        }
    }

    void Go()
    {
        foreach(GameObject dot in activates)
        {
            dot.SendMessage("Use");
            dot.SendMessage("Go");
        }
    }

    void ChangeMusic()
    {
        music.SetActive(true);
        bossMusic.SetActive(false);
    }
}
