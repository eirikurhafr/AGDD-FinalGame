using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScrollController : MonoBehaviour
{
    public string keyName;
    private bool open = false;

    private void Use(PlayerController player)
    {
        foreach (string item in player.Inventory)
        {
            if (item == keyName)
            {
                open = true;
                break;
            }
        }
        if (open)
        {
            player.SendMessage("removeFromInventory", keyName);
            gameObject.SendMessage("Use");
        }
    }
}