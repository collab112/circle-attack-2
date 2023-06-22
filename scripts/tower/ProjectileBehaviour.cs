using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
	
    public float life = 10.0f;
	public int damageDealt;
	
	void Awake() {
		
		Destroy(gameObject, life);
		
	}
	
	void OnCollisionEnter2D(Collision2D collision2DVariable) {
		
		if ( collision2DVariable.gameObject.layer == 8 ) {

			// If the projectile hits an enemy, damage the enemy and destroy the projectile
			collision2DVariable.gameObject.GetComponent<Enemy>().takeDamageEnemy(damageDealt);
			Destroy(gameObject);
			
		}
		
	}
	
}
