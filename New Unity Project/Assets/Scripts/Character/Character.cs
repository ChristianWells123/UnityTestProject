using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public float walkSpeed;
	public float runSpeed;
	public float gravity;
	public float snapDistance;

	protected float moveX;
	protected float moveY;
	protected float moveZ;

	public int maxHP;
	public int currentHP;
	public int centerHP;

	public bool isPaused;
	public bool isRunning;
	public bool inBattle;

	public CharacterController character;
	public Animator animator;
	

	public enum AnimationDirection {
		None, North, East, South, West
	};

	protected AnimationDirection lastDirection;


	// Use this for initialization
	void Start () {
		isPaused = false;

		moveX = 0;
		moveY = 0;
		moveZ = 0;

		lastDirection = AnimationDirection.None;

		centerHP = maxHP / 2;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public AnimationDirection getDirection(Vector2 directionVector)
	{
		if (directionVector == Vector2.zero) {
			return AnimationDirection.None;
		}

		float angle = 0;

		if (directionVector.x < 0) {
			angle = 360 - (Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg * -1);
		}
		else {
			angle = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
		}

		if (angle > 315 || angle < 45) return AnimationDirection.North;          // North: 0 deg +- 45
		else if (angle >= 45 && angle <= 135) return AnimationDirection.East;    // East: 90 deg +- 45 
		else if (angle > 135 && angle < 225) return AnimationDirection.South;    // South: 180 deg +- 45
		else if (angle >= 225 && angle <= 315) return AnimationDirection.West;   // West: 270 deg +- 45
		return AnimationDirection.None;
	}

	public void updateAnimationDirection(AnimationDirection movementDirection){	
		if (movementDirection == AnimationDirection.North) animator.SetTrigger("runUp");
		else if (movementDirection == AnimationDirection.East) animator.SetTrigger("runRight");
		else if (movementDirection == AnimationDirection.South) animator.SetTrigger("runDown");
		else if (movementDirection == AnimationDirection.West) animator.SetTrigger("runLeft");
		else if (movementDirection == AnimationDirection.None) animator.SetTrigger("notRunning");
	}

	public void updatePosition(float walkX, float walkZ) {
		if(character.isGrounded) {
			moveY = 0;
		}
		else {
			moveY -= gravity;
		}
		

		Vector2 walkVector = new Vector2(walkX, walkZ).normalized * runSpeed;

		moveX = walkVector.x;
		moveZ = walkVector.y;	// misleading: walkVector.y is the 
								// player's movement in the z direction

		AnimationDirection movementDirection = getDirection(walkVector);

		if (movementDirection != lastDirection) {
			updateAnimationDirection(movementDirection);
		}

		Vector3 displacement = new Vector3(moveX, moveY, moveZ);  

		character.Move(displacement * Time.deltaTime);

		RaycastHit hitInfo = new RaycastHit();                                                          // Check if the player
		if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hitInfo, snapDistance)) {    // is snapDistance units
			character.Move(new Vector3(0, snapDistance * -1, 0));                                       // above a surface. If 
		}                                                                                               // they are, snap them to it.

		lastDirection = movementDirection;
	}

	public void updatePosition(Vector3 movementVector) {
		updatePosition(movementVector.x, movementVector.z);
	}

	public void updatePosition(Vector2 inputVector) {
		updatePosition(inputVector.x, inputVector.y);
	}

	public void increaseHealth(int x)
	{
		currentHP += x;
		if (currentHP >= maxHP) {
			currentHP = maxHP;
		}
	}

	public void decreaseHealth(int x)
	{
		currentHP -= x;
		if (currentHP <= 0) {
			currentHP = 0;
		}
	}

	public void setHealth(int x)
	{
		currentHP = x;
		if (currentHP <= 0) {
			currentHP = 0;
		}
		if (currentHP >= maxHP) {
			currentHP = maxHP;
		}
	}

	public void onPauseGame(){
		isPaused = true;
		animator.enabled = false;
	}

	public void onResumeGame(){
		isPaused = false;
		animator.enabled = true;
	}
}
