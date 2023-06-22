using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TowerPlacement : MonoBehaviour
{

	public Tilemap grass;
	public Tilemap path;
	
	// Flag variable and GameObject variable to handle the prefab and placement of towers
	public bool towerBeingPlaced;					
	GameObject tower;

	// Variables for the OverlapCollider() call
	public ContactFilter2D whatIsTower; // Contains a ContactFilter2D that uses a mixed layermask of path and tower to filter for
	private CircleCollider2D towerCollider;
	private Collider2D[] results = new Collider2D[10]; 

	// Setting the playbutton active or not
	public Button playButton;


	void Awake() {

		playButton = (GameObject.Find("Play")).GetComponent<Button>();
		towerBeingPlaced = false;

	}
	

	bool checkIfValid(Vector3 inputPos) {

		Vector3 tempVector = new Vector3 ( inputPos.x + 9, inputPos.y + 5, 0 );
		bool placeable = true;

		if (towerCollider.OverlapCollider(whatIsTower, results) != 0) { 

			/*/ / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / 
			*
			* The OverlapCollider() function returns an integer, specifically
			* the amount of colliders overlapping with the tower's collider
			* that follow the specified contact filter. In this case, the 
			* contact filter filters for other towers or paths meaning if
			* there is a path or other tower colliding with the tower that is
			* about to be placed, render it unplaceable
			*
			/ / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /*/

			placeable = false;

		} else {
			
			placeable = true;

		}
		
		if (tempVector.x >= 15) { // x >= 15 is covered by the tower placement UI, so this place should not be placeable
						
			placeable = false;
						
		} 
		
		return placeable;
		
	}
	

	void Update() {
		
		if ( towerBeingPlaced ) {

			tower.transform.position = removeZ(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			
			if ( Input.GetMouseButtonDown(0) ) {
				
				if ( checkIfValid(removeZ(Camera.main.ScreenToWorldPoint(Input.mousePosition))) ) {
						
					// If LMB was clicked and the position is valid, re-enable the tower's scripts
					// and re-enable the play button and reset the tower and towerCollider variable
					tower.GetComponent<TowerBehaviour>().enabled = true;
					tower.GetComponent<TowerClick>().enabled = true;
					playButton.enabled = true;
					towerBeingPlaced = false;
					tower = null;
					towerCollider = null;

				}
				
			} else if (Input.GetMouseButtonDown(1)) {

					// If RMB was clicked, refund the tower and destroy it, and reset all the variables
					// and also re-enable the play button
					gameObject.GetComponent<Cash>().updateCash(tower.GetComponent<TowerBehaviour>().value);
					Destroy(tower);
					towerBeingPlaced = false;
					tower = null;
					towerCollider = null;
					playButton.enabled = true;
					
			}
			
		}
		
	}
	

	public Vector3 removeZ(Vector3 inputPos) { 
		
		return new Vector3(inputPos.x, inputPos.y, 0);
		
		/*/ / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / 
		*
		* The purpose of this function was to solve a logic error where the
		* sprite would be unable to be seen because the Z coordinate was 
		* aligned with the camera's Z coordinate, so it was not in front of it
		* and so could not be rendered onto it. To solve it, this function
		* sets the Z to 0 which is in front of the camera
		*
		/ / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /*/

	}
	

	public void placeTower(GameObject towerPrefab) {

		if( gameObject.GetComponent<Cash>().updateCash(-(towerPrefab.GetComponent<TowerBehaviour>().value)) ) {
		
			towerBeingPlaced = true;
			
			if ( tower == null ) { 
				
				/*/ / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /  
				*
				* Check needed due to a logic error where the player would lose the tower money
				* if a tower was being placed and the user clicked on another tower to buy, so this if statement
				* will check if the tower variable is empty (no tower is selected)
				*
				/ / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /*/

				// Initiate an instance of a tower prefab and set it to the mouse position, disable 
				// its scripts so it does not shoot while being placed and turn off the playbutton
				tower = Instantiate(towerPrefab, removeZ(Camera.main.ScreenToWorldPoint(Input.mousePosition)), Quaternion.identity);
				towerCollider = tower.GetComponent<CircleCollider2D>();
				tower.GetComponent<TowerBehaviour>().enabled = false;
				tower.GetComponent<TowerClick>().enabled = false;
				playButton.enabled = false;
			
			} else {
				
				// If there was a tower before, refund it and destroy the gameobject then run the above code to get a new tower
				gameObject.GetComponent<Cash>().updateCash(tower.GetComponent<TowerBehaviour>().value);
				Destroy(tower);
				tower = Instantiate(towerPrefab, removeZ(Camera.main.ScreenToWorldPoint(Input.mousePosition)), Quaternion.identity);
				towerCollider = tower.GetComponent<CircleCollider2D>();
				tower.GetComponent<TowerBehaviour>().enabled = false;
				tower.GetComponent<TowerClick>().enabled = false;
				playButton.enabled = false;
				
			}

		}

	}

}
