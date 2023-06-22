using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	
	public int health;
	public int value;

	private bool accessor; // use this boolean to access the updateCash() function of the worldHandler as it is not a method; it needs to be assigned to a variable
	
	public GameObject worldHandler;
	
	void Awake() {
		
		worldHandler = GameObject.Find("WorldHandler");
		
	}
	
	public void takeDamageEnemy(int damage) {
		
		health -= damage;
	
	}
		
	void Update() {
		
		if ( health < 0 ) {
			
			Destroy(gameObject);
			accessor = worldHandler.GetComponent<Cash>().updateCash(value);
			worldHandler.GetComponent<Score>().updateScore(value);
			
			// If the health of the enemy is less than 0, destroy the enemy game object and update the cash and score of the player

		}
		
	}
		
}
