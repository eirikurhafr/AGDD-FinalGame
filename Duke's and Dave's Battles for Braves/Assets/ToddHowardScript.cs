using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToddHowardScript : MonoBehaviour {
    public StopThrowNPC throwBool;
    public BattleCabin battleBool;
    public GameObject Gate;

	// Update is called once per frame
	void Update () {
		if(throwBool.done && battleBool.done)
        {
            Gate.SendMessage("Use");
        }
	}
}
