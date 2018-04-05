using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour {

    public string EscButton;
    public GameObject fullCanvas;
    public GameObject otherCanvas;
    public GameObject thirdCanvas;
    public GameObject selButton;
    private int originalPirority;
    private bool check;
    EventSystem m_EventSystem;


    // Use this for initialization
    void Start () {
        fullCanvas.SetActive(false);
        check = true;
        m_EventSystem = EventSystem.current;
    }
	
	// Update is called once per frame
	void Update () {
		if(CrossPlatformInputManager.GetButtonDown(EscButton))
        {
            if(check) {
                Time.timeScale = 0;
                fullCanvas.SetActive(true);
                otherCanvas.SetActive(false);
                thirdCanvas.SetActive(false);
                m_EventSystem.SetSelectedGameObject(selButton);
                check = !check;
            }
            else
            {
                Time.timeScale = 1;
                fullCanvas.SetActive(false);
                otherCanvas.SetActive(true);
                thirdCanvas.SetActive(true);
                check = !check;
            }
        }
	}

    public void returnToGame()
    {
        Time.timeScale = 1;
        fullCanvas.SetActive(false);
        otherCanvas.SetActive(true);
        thirdCanvas.SetActive(true);
        check = !check;
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
