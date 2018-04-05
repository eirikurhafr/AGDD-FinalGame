using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameoverscript2 : MonoBehaviour {

	public void quitGame()
    {
        SceneManager.LoadScene("menuScene");
    }
}
