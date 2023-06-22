using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{

	// Edits the text at the top to display the remaining life
	public Text lifeText;
	public int health = 50;
	
	public GameObject panel;
	
	public Button button1;
	public Button button2;
	public Button button3;

	public Button playButton;
	

	void Awake() {
		
		lifeText.text = "LIFE: " + health.ToString(); 
		
	}

	
	public void takeDamage(int damage) {

		if ( (health -= damage) <= 0 ) { // If the incoming damage will make the health 0 or less, run this code

			// Set the life to 0 and start calculating score, set the game over panel active and turn off the buttons
			lifeText.text = "LIFE: 0";

			gameObject.GetComponent<Score>().getFinalScore(false);

			panel.SetActive(true); 

			button1.GetComponent<Button>().enabled = false;
			button2.GetComponent<Button>().enabled = false;
			button3.GetComponent<Button>().enabled = false;

			playButton.enabled = false;
		
		} else {

			health -= damage;
			lifeText.text = "LIFE: " + health.ToString();

		}
		
	}
	
}
