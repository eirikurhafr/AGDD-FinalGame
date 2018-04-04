using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFinalBossScript : MonoBehaviour
{
    public StuckBoi boss;
    public GameObject[] activates;
    private GameObject music;
    public GameObject bossMusic;
    public GameObject cutscene;
    bool once = false;

    private void Start()
    {
        music = GameObject.Find("Music");
    }

    private void Update()
    {
        if (boss.health <= 0 && !once)
        {
            Go();
            ChangeMusic();
            once = true;
        }
    }

    void Go()
    {
        foreach (GameObject dot in activates)
        {
            dot.SendMessage("Use");
        }
        cutscene.SetActive(true);
    }

    void ChangeMusic()
    {
        music.SetActive(true);
        bossMusic.SetActive(false);
    }
}
