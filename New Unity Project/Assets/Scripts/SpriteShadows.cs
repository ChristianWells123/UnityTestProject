using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadows : MonoBehaviour {

	public bool receiveShadows = true;
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
		GetComponent<Renderer>().receiveShadows = receiveShadows;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
