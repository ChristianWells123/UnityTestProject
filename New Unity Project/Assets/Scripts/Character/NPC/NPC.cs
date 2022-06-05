using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character {

	public float randomMovementSecondsLowerBound;
	public float randomMovementSecondsUpperBound;

	public float randomMomvementProbability;
	
	public float sightDistance;

	public GameObject playerCharacter;

	public Vector2 dirMoving;

	protected float moveSeconds;

	// Use this for initialization
	void Start () {
		moveSeconds = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void moveRandomly() {
		if (UnityEngine.Random.Range(0, randomMomvementProbability) <= 1) {
			int rand = (int)Mathf.Floor(UnityEngine.Random.Range(0, 4));
			if (rand == 0)
				dirMoving = Vector2.up;
			if (rand == 1)
				dirMoving = Vector2.right;
			if (rand == 2)
				dirMoving = Vector2.down;
			if (rand == 3)
				dirMoving = Vector2.left;
			moveSeconds = UnityEngine.Random.Range(randomMovementSecondsLowerBound, randomMovementSecondsUpperBound);
		}

		updatePosition(dirMoving);

		moveSeconds -= Time.deltaTime;

		if (moveSeconds <= 0)
			dirMoving = Vector2.zero;
	}

	public bool isPlayerInSight() {
		return Vector3.Distance(playerCharacter.transform.position, transform.position) <= sightDistance;
	}

	protected void moveTowardPlayer() {
		updatePosition(playerCharacter.transform.position - transform.position);
	}

	protected void moveAwayFromPlayer() {
		updatePosition(transform.position - playerCharacter.transform.position);
	}
}
