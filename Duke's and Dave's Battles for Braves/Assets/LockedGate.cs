using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedGate : MonoBehaviour {
    public string keyName;
    public GameObject thingToDestroy;
    private bool open = false;

	// Use this for initialization
	private void Use(PlayerController player)
    {
        foreach(string item in player.Inventory)
        {
            if(item == keyName)
            {
                open = true;
                break;
            }
        }
        if(open)
        {
            openGate();
            if(thingToDestroy)
            {
                Destroy(thingToDestroy);
            }
            player.SendMessage("removeFromInventory", keyName);
            Destroy(gameObject);
        }
    }

    private void openGate()
    {
        Debug.Log("Gate Opened");

    }

}
