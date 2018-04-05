using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameoverscript : MonoBehaviour {
    private PlayerController player1;
    private PlayerController player2;
    public GameObject[] disable;
    public GameObject enable;
	// Use this for initialization
	void Start () {
        player1 = GameObject.Find("Player_1").GetComponent<PlayerController>();
        player2 = GameObject.Find("Player_2").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		if(player1.health <= 0 && player2.health <= 0)
        {
            Time.timeScale = 0;
            foreach (GameObject dis in disable)
            {
                dis.SetActive(false);
            }
            enable.SetActive(true);
        }
	}
}
