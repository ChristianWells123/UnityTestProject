using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public Slider healthBar;
	public Slider midBar;

	public Character character;

	// Use this for initialization
	void Start () {
		
		healthBar.maxValue = character.maxHP;

		midBar.maxValue = character.maxHP;

		midBar.value = character.centerHP;
		
	}
	
	// Update is called once per frame
	void Update () {

		healthBar.value = character.currentHP;
		
	}
}
