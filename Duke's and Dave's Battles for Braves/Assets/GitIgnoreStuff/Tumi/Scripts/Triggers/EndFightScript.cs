using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFightScript : MonoBehaviour {
    public SimpleEnemy[] enemies;
    private int count;
    public GameObject[] activate;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        count = 0;
		foreach(SimpleEnemy enemy in enemies)
        {
            if(enemy.dead)
            {
                count++;
            }
        }

        if(enemies.Length == count)
        {
            foreach (GameObject obj in activate)
            {
                obj.SendMessage("Activate");
            }
            Destroy(gameObject);
        }
	}
}
