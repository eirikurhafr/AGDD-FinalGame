using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCabin : MonoBehaviour {
    public SimpleEnemy[] enemies;
    public bool done = false;
	
	// Update is called once per frame
	void Update () {
        if(enemies[0].health <= 0 && enemies[1].health <= 0 && enemies[2].health <= 0)
        {
            done = true;
        }
	}
}
