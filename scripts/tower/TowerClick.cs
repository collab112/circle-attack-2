using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerClick : MonoBehaviour
{
	
	public GameObject worldHandler;
	
	void Awake() {
		
		worldHandler = GameObject.Find("WorldHandler");
		
	}
	
	void OnMouseDown() {

		// If the tower is clicked, first check that the tower instance is already placed and if it is, display the range overlay
		if ( worldHandler.GetComponent<TowerPlacement>().towerBeingPlaced == false) {
			gameObject.transform.GetChild(0).gameObject.SetActive(!gameObject.transform.GetChild(0).gameObject.activeSelf);
		}

	}
	
}
