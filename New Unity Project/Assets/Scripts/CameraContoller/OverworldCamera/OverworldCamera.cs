using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCamera : CameraController {

	public float cameraBoundsXMin;
	public float cameraBoundsXMax;
	public float cameraBoundsZMin;
	public float cameraBoundsZMax;

	public GameObject player;

	private Vector3 offsetFromPlayer;

	// Use this for initialization
	void Start () {
		offsetFromPlayer = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 updatePosition = player.transform.position + offsetFromPlayer;
		if (updatePosition.x >= cameraBoundsXMax) {
			updatePosition.x = cameraBoundsXMax;
		}
		if (updatePosition.x <= cameraBoundsXMin) {
			updatePosition.x = cameraBoundsXMin;
		}
		if (updatePosition.z >= cameraBoundsZMax) {
			updatePosition.z = cameraBoundsZMax;
		}
		if (updatePosition.z <= cameraBoundsZMin) {
			updatePosition.z = cameraBoundsZMin;
		}

		transform.position = updatePosition;
	}
}
