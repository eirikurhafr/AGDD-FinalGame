﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

	public void StartGame()
	{
		SceneManager.LoadScene("NewLevelScene");
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
