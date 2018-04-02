using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneCameraScript : MonoBehaviour {
    public string dialogue;
    private Text dialogueText;
    private Image player1;
    private Image player2;
    private Image npc;
    private GameObject player1GO;
    private GameObject player2GO;
    public string player;
    public string action;

	// Use this for initialization
	void Start () {
        dialogueText = GameObject.Find("Dialogue Text").GetComponent<Text>();

        player1 = GameObject.Find("Player1Image").GetComponent<Image>();
        player2 = GameObject.Find("Player2Image").GetComponent<Image>();
        npc = GameObject.Find("NPCImage").GetComponent<Image>();

        player1.enabled = false;
        player2.enabled = false;
    }

    void Go(GameObject[] puppets)
    {
        player1.enabled = false;
        player2.enabled = false;
        npc.enabled = false;
        dialogueText.text = dialogue;

        switch(action)
        {
            case "SittingGround":
                if (player == "player1")
                    puppets[0].SendMessage("SittingGround");
                else if (player == "player2")
                    puppets[1].SendMessage("SittingGround");
                else if (player == "npc")
                    puppets[2].SendMessage("SittingGround");
                break;
            case "Thinking":
                if (player == "player1")
                    puppets[0].SendMessage("Thinking");
                else if (player == "player2")
                    puppets[1].SendMessage("Thinking");
                else if (player == "npc")
                    puppets[2].SendMessage("Thinking");
                break;
            case "Pointing":
                if (player == "player1")
                    puppets[0].SendMessage("Pointing");
                else if (player == "player2")
                    puppets[1].SendMessage("Pointing");
                else if (player == "npc")
                    puppets[2].SendMessage("Pointing");
                break;
            case "Waving":
                if (player == "player1")
                    puppets[0].SendMessage("Waving");
                else if (player == "player2")
                    puppets[1].SendMessage("Waving");
                else if (player == "npc")
                    puppets[2].SendMessage("Waving");
                break;
            case "Backflip":
                if (player == "player1")
                    puppets[0].SendMessage("Backflip");
                else if (player == "player2")
                    puppets[1].SendMessage("Backflip");
                else if (player == "npc")
                    puppets[2].SendMessage("Backflip");
                break;
            case "Stop":
                if (player == "player1")
                    puppets[0].SendMessage("Stop");
                else if (player == "player2")
                    puppets[1].SendMessage("Stop");
                else if (player == "npc")
                    puppets[2].SendMessage("Stop");
                break;
            case "RunFlip":
                if (player == "player1")
                    puppets[0].SendMessage("RunFlip");
                else if (player == "player2")
                    puppets[1].SendMessage("RunFlip");
                else if (player == "npc")
                    puppets[2].SendMessage("RunFlip");
                break;
            case "HoldingBox":
                if (player == "player1")
                    puppets[0].SendMessage("HoldingBox");
                else if (player == "player2")
                    puppets[1].SendMessage("HoldingBox");
                else if (player == "npc")
                    puppets[2].SendMessage("HoldingBox");
                break;
            case "Dancing":
                if (player == "player1")
                    puppets[0].SendMessage("Dancing");
                else if (player == "player2")
                    puppets[1].SendMessage("Dancing");
                else if (player == "npc")
                    puppets[2].SendMessage("Dancing");
                break;
            case "ClearBools":
                if (player == "player1")
                    puppets[0].SendMessage("ClearBools");
                else if (player == "player2")
                    puppets[1].SendMessage("ClearBools");
                else if (player == "npc")
                    puppets[2].SendMessage("ClearBools");
                break;
            default:
                break;
        }

        if (player == "player1")
            player1.enabled = true;
        else if (player == "player2")
            player2.enabled = true;
        else
            npc.enabled = true;
    }
}
