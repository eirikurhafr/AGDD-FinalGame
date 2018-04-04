using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossFight : MonoBehaviour {
    private GameObject music;
    public GameObject bossMusic;
    private GameObject player1;
    private GameObject player2;
    public Vector3 newPositionP1;
    public Vector3 newPositionP2;
    public GameObject cutscene;
    private bool first = true;
    // Use this for initialization
    void Start () {
        player1 = GameObject.Find("Player_1");
        player2 = GameObject.Find("Player_2");
        music = GameObject.Find("Music");
    }
	
	void OnTriggerEnter(Collider collision)
    {
        if(first && (collision.tag == "Player_1" || collision.tag == "Player_2"))
        {
            music.SetActive(false);
            bossMusic.SetActive(true);
            player1.transform.position = newPositionP1;
            player2.transform.position = newPositionP2;
            if (cutscene)
                cutscene.SendMessage("StartScene");
            first = false;
        }
    }
}
