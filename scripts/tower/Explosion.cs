using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	
	Collider2D[] enemiesInRange;
	public LayerMask whatIsEnemy;
	public float radius;
	public int damage;
	

    void Start() {
	
		Destroy(gameObject, .1f);
		damageEnemies();
	
    }

	void damageEnemies() {

		enemiesInRange = Physics2D.OverlapCircleAll(transform.position, radius, whatIsEnemy);
		
		for ( int i = 0; i < enemiesInRange.Length; i++ ) {

			// Damage all enemies in the range of the explosion			
			enemiesInRange[i].gameObject.GetComponent<Enemy>().takeDamageEnemy(damage);
			
		}
		
	
	}
}
