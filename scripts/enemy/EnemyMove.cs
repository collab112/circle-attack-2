using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
	
	public float velocity;
	public float distanceLeft;
	public int damage;
	
	public GameObject roamingNodesParent;
	private List<Transform> roamingNodes = new List<Transform>();

	private Rigidbody2D rb;
	Vector2 direction;
	
	public GameObject worldHandler;

	int i;
	

	void Awake() {
		
		rb = gameObject.GetComponent<Rigidbody2D>();
		
		roamingNodesParent = GameObject.Find("MapNodes");
		worldHandler = GameObject.Find("WorldHandler");
		
		for (int i = 0; i < roamingNodesParent.transform.childCount; i++) {
			
			// Gather all the nodes and put them into the roamingNodes list
			roamingNodes.Add(roamingNodesParent.transform.GetChild(i)); 
			
		}
		
		for (int i = 0; i < roamingNodes.Count-1; i++) { 
			
			// Gather the distance between all nodes and sum them into a single float
			distanceLeft += (roamingNodes[i+1].position - roamingNodes[i].position).magnitude;
			
		}
		
	}
	

	void Start() {
		
		// "Spawn" the enemy at the first node
		transform.position = roamingNodes[0].transform.position;
		
		// Retrieve a "priming" velocity to start off before Update()
		direction = new Vector2(roamingNodes[1].position.x - transform.position.x, roamingNodes[1].position.y - transform.position.y);
		direction = BetterNormalisedVector(direction.x, direction.y);
		rb.velocity = velocity * direction;
		
		i = 1;
		
	}
	

	public Vector2 BetterNormalisedVector(float x, float y) {
		
		float vectorMagnitude = Mathf.Sqrt( x*x + y*y );
		float newX = x/vectorMagnitude;
		float newY = y/vectorMagnitude;
		
		return new Vector2(newX, newY);

		/*/ / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / 
		*
		* After debugging a bit I discovered the default unity normalisation 
		* was actually quite inaccurate, and researching online says there were
		* many problems with it, so I decided to make my own normalisation
		* function
		*
		/ / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /*/
		
	}
	

	void moveToPoints() {

		// If the enemy position is within .07 of a node (accounting for floating point inaccuracy and/or quantisation)
		if ( Mathf.Abs(roamingNodes[i].position.x - transform.position.x) < 0.07 && Mathf.Abs(roamingNodes[i].position.y - transform.position.y) < 0.07 ) {	
			
			// If there are still nodes left
			if ( i != roamingNodes.Count - 1) {
				
				i++;
			
				// Increment i and get a new velocity vector
				direction = new Vector2(roamingNodes[i].position.x - transform.position.x, roamingNodes[i].position.y - transform.position.y);
				direction = BetterNormalisedVector(direction.x, direction.y);
				rb.velocity = velocity * direction;
			
			} else { 
				
				// Last node has been reached, destroy the enemy game object and damage the player
				worldHandler.GetComponent<Life>().takeDamage(damage);
				Destroy(gameObject);
				
			}
				
		}

		// Decrease distanceLeft by the distance travelled
		distanceLeft -= velocity * Time.deltaTime; 

		if ( worldHandler.GetComponent<Life>().health <= 0 ) {

			Destroy(gameObject);

			/*/ / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /
			*
			* This section of code was needed to fix a very confusing logic
			* error where the enemies would continue to go through to the end
			* even if all lives were lost, so I decided to just destroy
			* all the enemies if all lives were lost
			* 
			/ / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /*/

		}

	}


	void Update() {

		moveToPoints();

	}
	
}

