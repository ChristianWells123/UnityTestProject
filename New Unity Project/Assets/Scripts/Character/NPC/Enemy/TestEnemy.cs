using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TestEnemy : Enemy {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPaused && !inBattle) {	
			if (isPlayerInSight()){
				moveTowardPlayer();
			}
			else {
				moveRandomly();
			}
		}
	}
}
