using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

	protected bool lastFramePauseButton;
	protected bool currentPauseButton;

	public bool isPaused;

	public GameObject pauseScreen;

	// Use this for initialization
	void Start () {
		lastFramePauseButton = false;
		currentPauseButton = false;
		isPaused = false;
		pauseScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		currentPauseButton = Input.GetAxis("Cancel") > 0;
		if (currentPauseButton && !lastFramePauseButton) {
			Object[] objects = FindObjectsOfType(typeof(GameObject));
			foreach (GameObject go in objects) {
				if (isPaused) {
					go.SendMessage("onResumeGame", SendMessageOptions.DontRequireReceiver);
					pauseScreen.SetActive(false);
				}
				else {
					go.SendMessage("onPauseGame", SendMessageOptions.DontRequireReceiver);
					pauseScreen.SetActive(true);
				}
			}
			isPaused = !isPaused;
		}
		lastFramePauseButton = currentPauseButton;
	}
}
