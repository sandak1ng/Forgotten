using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

	public GameObject PauseMenu;

	private bool paused = false;

	void Start () 
	{
		PauseMenu.SetActive (false);
	}

	void Update () 
	{
		if (Input.GetButtonDown ("Pause")) 
		{
			paused = !paused;
		}

		if (paused) 
		{
			PauseMenu.SetActive (true);
			Time.timeScale = 0;
		}

		if (!paused) 
		{
			PauseMenu.SetActive (false);
			Time.timeScale = 1;
		}
	}

	public void Continue () {
		paused = false;
	}

	public void Exit () {
		Application.Quit ();
	}
}
