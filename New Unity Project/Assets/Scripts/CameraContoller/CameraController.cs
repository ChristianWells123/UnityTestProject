using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public bool isPaused;

	// public enum VelocityFunction {
	// 	CosineFullPeriod, InvertedCosineFullPeriod, 
	// 	CosineHalfPeriod, InvertedCosineHalfPeriod, 
	// 	Quadratic, InvertedQuadratic,
	// 	None
	// };

	public enum Axis {
		X, Y, Z
	};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public delegate float VelocityFunction (float x);

	public float Constant(float x){
		return 1;
	}
	public float CosineFullPeriod(float x){
		return (-1 * Mathf.Cos(x * Mathf.PI * 2) + 1);
	}
	public float InvertedCosineFullPeriod(float x){
		return (Mathf.Cos(x * Mathf.PI * 2) + 1);
	}
	public float CosineHalfPeriod(float x){
		return (Mathf.Cos(x * Mathf.PI) + 1);
	}
	public float InvertedCosineHalfPeriod(float x){
		return (-1 * Mathf.Cos(x * Mathf.PI) + 1);
	}
	public float Quadratic(float x) {
		return (-1 * Mathf.Pow((Mathf.Sqrt(2) * 2 * x - Mathf.Sqrt(2)), 2) + 2) / (4/3);
	}
	public float InvertedQuadratic(float x) {
		return (2 * Mathf.Pow((Mathf.Sqrt(2) * 2 * x - Mathf.Sqrt(2)), 2)) / (4/3);
	}
	public float Linear(float x) {
		return 2*x;
	}

	///Pans the camera horozontally from an angle reletive to the current camera angle.
	///angleFromCurrent: horozontal angle from current camera position to pan to.
	///time: time in seconds to pan.

	public void PanRelative(float angleFromCurrent, float time, VelocityFunction velocFunction) {
		StartCoroutine(RotateOverTime(angleFromCurrent, time, Vector3.up, velocFunction, false));
	}

	public void PanAbsolute(float angleFromCurrent, float time, VelocityFunction velocFunction) {
		StartCoroutine(RotateOverTime(angleFromCurrent, time, Vector3.up, velocFunction, true));
	}

	public void TiltRelative(float angleFromCurrent, float time, VelocityFunction velocFunction) {
		StartCoroutine(RotateOverTime(angleFromCurrent, time, Vector3.right, velocFunction, false));
	}

	public void TiltAbsolute(float angleFromCurrent, float time, VelocityFunction velocFunction) {
		StartCoroutine(RotateOverTime(angleFromCurrent, time, Vector3.right, velocFunction, true));
	}

	public void RollRelative(float angleFromCurrent, float time, VelocityFunction velocFunction) {
		StartCoroutine(RotateOverTime(angleFromCurrent, time, Vector3.forward, velocFunction, false));
	}

	public void RollAbsolute(float angleFromCurrent, float time, VelocityFunction velocFunction) {
		StartCoroutine(RotateOverTime(angleFromCurrent, time, Vector3.forward, velocFunction, true));
	}
	public void RotateRelative(float angleFromCurrent, float time, Vector3 rotationAxis, VelocityFunction velocFunction) {
		StartCoroutine(RotateOverTime(angleFromCurrent, time, rotationAxis, velocFunction, false));
	}

	public void RotateAbsolute(float angleFromCurrent, float time, Vector3 rotationAxis, VelocityFunction velocFunction) {
		StartCoroutine(RotateOverTime(angleFromCurrent, time, rotationAxis, velocFunction, true));
	}

	public void OrbitAndFocus(GameObject focusObject, float angle, float time, Vector3 rotationAxis, VelocityFunction velocFunction) {
		StartCoroutine(OrbitAndFocusOverTime(focusObject, angle, time, rotationAxis, velocFunction));
	}

	// float getVelocityModifier (VelocityFunction func, float progress) {
	// 	if (func == VelocityFunction.CosineFullPeriod){
	// 		return (-1 * Mathf.Cos(progress * Mathf.PI * 2) + 1);
	// 	}
	// 	else if (func == VelocityFunction.InvertedCosineFullPeriod){
	// 		return (Mathf.Cos(progress * Mathf.PI * 2) + 1);
	// 	}
	// 	else if (func == VelocityFunction.CosineHalfPeriod){
	// 		return (Mathf.Cos(progress * Mathf.PI) + 1);
	// 	}
	// 	else if (func == VelocityFunction.InvertedCosineHalfPeriod){
	// 		return (-1 * Mathf.Cos(progress * Mathf.PI) + 1);
	// 	}
	// 	else if (func == VelocityFunction.Quadratic) {
	// 		return (-1 * Mathf.Pow((Mathf.Sqrt(2) * 2 * progress - Mathf.Sqrt(2)), 2) + 2) / (4/3);
	// 	}
	// 	else if (func == VelocityFunction.InvertedQuadratic) {
	// 		return (2 * Mathf.Pow((Mathf.Sqrt(2) * 2 * progress - Mathf.Sqrt(2)), 2)) / (4/3);
	// 	}
	// 	return 1;
	// }
	
	
	IEnumerator OrbitAndFocusOverTime(GameObject focusObject, float angle, float time, Vector3 rotationAxis, VelocityFunction velocFunction) {
		float endTime = Time.time + time;
		float progress = 0;
		float velocityModifier = 1;

		Vector3 focusPosition = focusObject.transform.position;
		
		Vector3 lastFocusPosition = focusPosition;

		transform.LookAt(focusPosition);		
		
		while (Time.time <= endTime) {
			if (!isPaused) {
				
				focusPosition = focusObject.transform.position;
				progress = 1 - ((endTime - Time.time ) / time);
				
				velocityModifier = velocFunction(progress);

				transform.Translate((focusPosition - lastFocusPosition));

				transform.RotateAround(focusPosition, rotationAxis, (angle * Time.deltaTime) * velocityModifier / time);
				transform.LookAt(focusPosition);
				
				lastFocusPosition = focusPosition;

				yield return null;
		 
			}
			else {
				endTime += Time.deltaTime;
				yield return null;
			}
		}
	} 

	IEnumerator RotateOverTime (float angle, float time, Vector3 rotationAxis, VelocityFunction velocFunction, bool absoluteRotation = true) {
		float endTime = Time.time + time;
		float velocityModifier = 1;
		float progress = 0;

		while (Time.time <= endTime) {
			if (!isPaused) {
				progress = 1 - ((endTime - Time.time ) / time);

				velocityModifier = velocFunction(progress);

				transform.Rotate(rotationAxis, (angle * Time.deltaTime) * velocityModifier / time, absoluteRotation ? Space.World : Space.Self);

				yield return null;
			}
			else {
				endTime += Time.deltaTime;
				yield return null;
			}
		} 
	}

	public void onPauseGame(){
		isPaused = true;
	}

	public void onResumeGame(){
		isPaused = false;
	}
}