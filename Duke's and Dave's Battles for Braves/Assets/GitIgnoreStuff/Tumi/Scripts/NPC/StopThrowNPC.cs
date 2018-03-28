using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopThrowNPC : MonoBehaviour {
    public GameObject[] activate;
    public Text textBox;

    void OnCollisionEnter(Collision theCollision)
    {
        theCollision.gameObject.SendMessage("StopNPC");
        if(textBox)
        {
            textBox.text = "Thank you, take this potion for the help you provided";
        }
        foreach (GameObject thing in activate)
        {
            thing.SetActive(true);
        }
    }
}
