using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : Character {

	private Vector2 lastInput;
	private Vector2 inputVector;

	private float inputX;
	private float inputY;


	// Use this for initialization
	void Start () {
		
	}

	
	// Update is called once per frame
	void Update () {
		inputX = Input.GetAxis("Horizontal");
		inputY = Input.GetAxis("Vertical");
		inputVector = new Vector2(inputX, inputY);

		if (isPaused) {
			if (lastInput != inputVector && inputVector != Vector2.zero) {
				animator.enabled = true;
				updatePosition(inputVector);
			}
			else {
				animator.enabled = false;
			}
		}
		else {
			updatePosition(inputVector);
		}

		lastInput = new Vector2(inputX, inputY);
	}
}
