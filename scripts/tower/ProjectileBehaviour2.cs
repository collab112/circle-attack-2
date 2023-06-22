using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour2 : MonoBehaviour
{
	
    public float life = 10.0f;
	public int damageDealt;
	public GameObject ExplosionPrefab;
	
	
	void Awake() {
		
		Destroy(gameObject, life);
		
	}
	

	void OnCollisionEnter2D(Collision2D collision2DVariable) {
		
		if ( collision2DVariable.gameObject.layer == 8 ) {
			
			// Special projectile for the third tower which explodes on impact
			collision2DVariable.gameObject.GetComponent<Enemy>().takeDamageEnemy(damageDealt);
			GameObject explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
			
		}
		
	}
	
}
