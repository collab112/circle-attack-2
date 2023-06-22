using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
	
	public float range;
	public LayerMask whatIsEnemy; // LayerMask that can filter through colliders to check if enemies are in range

	public float cooldown; // Firing cooldown
	private float remainingCooldown;
	
	public int value;
	
	Collider2D[] objectsInRange;
	
	void Start() {
		
		remainingCooldown = 0.0f; // Priming cooldown for the first shot to have no cooldown
		
	}

	public void turnTower() {

		// Get all enemies in range and put their colliders into objectsInRange
		objectsInRange = Physics2D.OverlapCircleAll(transform.position, range, whatIsEnemy); 
		
		Collider2D firstEnemy;
		firstEnemy = objectsInRange[0];
		
		Debug.Log(objectsInRange.Length);
		
		for ( int i = 1; i < objectsInRange.Length; i++ ) {
			
			// For all the enemies in range, find the first one (the one with the least distanceLeft)
			if ( objectsInRange[i].gameObject.GetComponent<EnemyMove>().distanceLeft
				<
				firstEnemy.gameObject.GetComponent<EnemyMove>().distanceLeft
			) {

				firstEnemy = objectsInRange[i];

			}

		}
		
		lookAtTarget(firstEnemy.gameObject);

	}
	
	public void lookAtTarget(GameObject enemy) { 
		
		// An implementation of LookAt in 2D space
		Quaternion rotation = Quaternion.LookRotation (
		
			enemy.transform.position
				- 
			transform.position, 
			
			transform.TransformDirection(Vector3.back)
			
		);
		
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
		
	}
	
	void Update() {
		
		// If there are any enemies in the range of the tower, turn the tower and start firing
		if ( Physics2D.OverlapCircle(transform.position, range, whatIsEnemy ) ) { 
		
			turnTower();
			
			remainingCooldown -= Time.deltaTime;
			
			if ( remainingCooldown < 0 ) {
			
				gameObject.GetComponent<Shoot>().shoot();
				remainingCooldown = cooldown;
		
			}
		
		}
	}
	
}
