using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

	public GameObject ControlsImage;
	public GameObject CloseControlsButton;
	private bool flip = true;

	void Start()
	{
		ControlsImage.SetActive(false);
		CloseControlsButton.SetActive(false);
	}

	public void StartGame()
	{
		SceneManager.LoadScene("NewLevelScene");
	}

	public void Controls()
	{
		ControlsImage.SetActive(flip);
		CloseControlsButton.SetActive(flip);
		flip = !flip;
		
	}

	public void CloseControls()
	{
		ControlsImage.SetActive(false);
		CloseControlsButton.SetActive(false);
		flip = true;
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
