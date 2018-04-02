using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;


public class CameraController : MonoBehaviour {
    public Camera[] camerasToSignal;
    private Canvas UICanvas;
    private Canvas DialougeCanvas;
    private int index = 1;
    private Camera mainCamera;
    private GameObject mainCameraGO;
    private bool activated = false;
    private bool finished = false;
    public GameObject[] puppets;

	// Use this for initialization
	void Start () {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        UICanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        DialougeCanvas = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
        DialougeCanvas.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(!finished && activated)
        {
            bool go = CrossPlatformInputManager.GetButtonDown("NextMessage_P1");
            if (go)
            {
                Go();
            }
        }
    }

    void Go()
    {
        if (index < camerasToSignal.Length)
        {
            camerasToSignal[index - 1].enabled = false;
            camerasToSignal[index].enabled = true;
            camerasToSignal[index].SendMessage("Go", puppets);
            index++;
        }
        else
        {
            camerasToSignal[index - 1].enabled = false;
            mainCamera.enabled = true;
            UICanvas.enabled = true;
            DialougeCanvas.enabled = false;
            foreach (GameObject puppet in puppets)
            {
                puppet.SetActive(false);
            }
            finished = true;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (!activated)
        {
            foreach (GameObject puppet in puppets)
            {
                puppet.SetActive(true);
            }
            mainCamera.enabled = false;
            camerasToSignal[0].enabled = true;
            camerasToSignal[0].SendMessage("Go", puppets);
            activated = true;
            UICanvas.enabled = false;
            DialougeCanvas.enabled = true;
        }   
    }
}
