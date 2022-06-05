using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : CameraController {

	public GameObject obj;

	// Use this for initialization
	void Start () {
		OrbitAndFocus(obj, -30, 5, Vector3.up, Linear);
		//PanAbsolute(150, 5, VelocityFunction.CosineFullPeriod);
	}
	
	// Update is called once per frame
	void Update () {		
	}
}