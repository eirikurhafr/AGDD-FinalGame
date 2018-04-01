using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneCameraScript : MonoBehaviour {
    public string dialogue;
    private Text dialogueText;
    private Image player1;
    private Image player2;
    private GameObject player1GO;
    private GameObject player2GO;
    public string player;
    public string action;

	// Use this for initialization
	void Start () {
        dialogueText = GameObject.Find("Dialogue Text").GetComponent<Text>();

        player1 = GameObject.Find("Player1Image").GetComponent<Image>();
        player2 = GameObject.Find("Player2Image").GetComponent<Image>();

        player1.enabled = false;
        player2.enabled = false;
    }

    void Go(GameObject[] puppets)
    {
        player1.enabled = false;
        player2.enabled = false;
        dialogueText.text = dialogue;

        switch(action)
        {
            case "SittingGround":
                if (player == "player1")
                    puppets[0].SendMessage("SittingGround");
                else if (player == "player2")
                    puppets[1].SendMessage("SittingGround");
                break;
            case "Thinking":
                if (player == "player1")
                    puppets[0].SendMessage("Thinking");
                else if (player == "player2")
                    puppets[1].SendMessage("Thinking");
                break;
            case "ClearBools":
                if (player == "player1")
                    puppets[0].SendMessage("ClearBools");
                else if (player == "player2")
                    puppets[1].SendMessage("ClearBools");
                break;
            default:
                break;
        }

        if (player == "player1")
            player1.enabled = true;
        else if (player == "player2")
            player2.enabled = true;
    }
}
